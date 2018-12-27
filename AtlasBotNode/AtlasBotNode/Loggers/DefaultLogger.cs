using AtlasBotNode.Helpers;
using Discord;
using System;
using System.Text;
using System.Threading.Tasks;

using RabbitMQ.Client;

namespace AtlasBotNode.Loggers
{
    public static class DefaultLogger
    {
        private static IConnectionFactory _factory = new ConnectionFactory() { HostName = "localhost" };
        public static async Task Logger(LogMessage message)
        {
            switch (message.Severity)
            {
                case LogSeverity.Critical:
                case LogSeverity.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    await LogErrorToOwner(message);
                    break;

                case LogSeverity.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;

                case LogSeverity.Info:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;

                case LogSeverity.Verbose:
                case LogSeverity.Debug:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
            }
            using (var connection = _factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(
                    queue: "logs",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var body = Encoding.UTF8.GetBytes(message.Message);

                channel.BasicPublish(
                    exchange: string.Empty,
                    routingKey: "logs",
                    basicProperties: null,
                    body: body);
            }

            Console.WriteLine($"{DateTime.Now,-19} [{message.Severity,8}] {message.Source}: {message.Message} {message.Exception}");
            Console.ResetColor();
        }

        private static async Task LogErrorToOwner(LogMessage message)
        {
            var bort = DiscordCommandHelper.Client.GetUser(111211693870161920);
            var embedBuilder = new EmbedBuilder();
            embedBuilder.WithColor(Color.Red);
            embedBuilder.WithTitle("Issue");
            var errorMessage = message.Message;
            if (errorMessage.Length > 1200)
                errorMessage = errorMessage.Substring(0, 1000);
            embedBuilder.AddField(message.Source, errorMessage);
            await bort.SendMessageAsync("", embed: embedBuilder.Build());
        }
    }
}