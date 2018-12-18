using System;
using System.Linq;
using System.Threading;
using AtlasBotCommander.Communication;
using AtlasBotCommander.Loggers;
using Discord.Rest;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace AtlasBotCommander
{
    using System;
    using System.Reflection;
    using System.Threading.Tasks;
    using Discord;
    using Discord.Commands;
    using Discord.WebSocket;
    using Microsoft.Extensions.DependencyInjection;

    namespace AtlasBotNode
    {
        public class Program
        {
            private NodeHub _nodeHub;
            private static DiscordSocketClient _client;
            private IServiceProvider _services;
            private static void Main(string[] args) => new Program().Start().GetAwaiter().GetResult();

            private async Task Start()
            {
                InjectDependencies();
                _nodeHub = _services.GetRequiredService<NodeHub>();
                _client = new DiscordSocketClient();
                await _client.LoginAsync(TokenType.Bot, "NDg5MTY5ODMwMzY5NDI3NDcw.Dnm2sQ.sSco-_V64lho2H3uKwTC19M9WMo");
                await _client.StartAsync();

                new Thread(() => { _nodeHub.Listen(); }).Start();
                await Task.Delay(-1);
            }

            public static async Task LogMessage(string message)
            {
                var server = _client.GetGuild(489170913082998805);
                var channel = server.GetChannel(489171456966787072);
                if (channel is IMessageChannel messageChannel)
                {
                    await messageChannel.SendMessageAsync(message);
                }
            }
            private void InjectDependencies()
            {
                var serviceCollection = new ServiceCollection();
                serviceCollection.AddTransient<NodeHub>();
                serviceCollection.AddSingleton<ILoggerFactory, LoggerFactory>();
                serviceCollection.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
                serviceCollection.AddLogging((builder) => builder.SetMinimumLevel(LogLevel.Trace));

                _services = serviceCollection.BuildServiceProvider();

                var loggerFactory = _services.GetRequiredService<ILoggerFactory>();
                loggerFactory.AddNLog(new NLogProviderOptions
                {
                    CaptureMessageTemplates = true,
                    CaptureMessageProperties = true
                });
                NLog.LogManager.LoadConfiguration("nlog.config");
            }
        }

    }

}
