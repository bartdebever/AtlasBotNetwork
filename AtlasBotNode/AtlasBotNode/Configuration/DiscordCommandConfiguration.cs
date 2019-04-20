using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace AtlasBotNode.Configuration
{
    public class DiscordCommandConfiguration
    {
        public static DiscordSocketClient Client { get; set; }

        public static CommandService CommandService { get; set; }

        public DiscordCommandConfiguration()
        {
            var commandServiceConfig = new CommandServiceConfig {DefaultRunMode = RunMode.Async};
            Client = new DiscordSocketClient();
            CommandService = new CommandService(commandServiceConfig);
        }

        public async Task Start(string apiKey)
        {
            await Client.LoginAsync(TokenType.Bot, apiKey);
            await Client.StartAsync();

            await Task.Delay(-1);
        }
    }
}
