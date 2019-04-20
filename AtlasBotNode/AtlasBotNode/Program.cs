using AtlasBotNode.EmbedGenerators;
using AtlasBotNode.EmbedGenerators.ModuleGenerators;
using AtlasBotNode.Helpers;
using AtlasBotNode.Loggers;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using AtlasBotNode.Configuration;
using AtlasBotNode.Modules;
using ChampionGgApiHandler;
using Microsoft.Extensions.Configuration;
using SpeedrunAPIHandler;
using YoutubeApiHandler;

namespace AtlasBotNode
{
    public class Program
    {
        private IServiceProvider _services;
        private static IConfiguration _config;

        private static void Main(string[] args)
        {
            _config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false).Build();
            new Program().Start().GetAwaiter().GetResult();
        }

        private async Task Start()
        {
            var discordCommandConfig = new DiscordCommandConfiguration();

            var section = _config.GetSection("keys");
            SetApiKeys(section);

            DiscordCommandConfiguration.Client.Log += DefaultLogger.Logger;
            DiscordCommandConfiguration.CommandService.Log += DefaultLogger.Logger;

            _services = new ServiceCollection()
                .AddEmbedGenerators()
                .BuildServiceProvider();

            DiscordCommandConfiguration.Client.MessageReceived += HandleCommand;
            //TODO Handle the message update;
            await CommandConfiguration.AddModules(_config.GetSection("modules"), _services);


            await discordCommandConfig.Start(_config.GetSection("keys:discord").Value);
        }

        private DiscordSocketClient Client => DiscordCommandConfiguration.Client;
        private CommandService Commands => DiscordCommandConfiguration.CommandService;

        // TODO Move to handle command classes
        private async Task HandleCommand(SocketMessage messageParam)
        {
            // Don't process the command if it was a System Message
            if (!(messageParam is SocketUserMessage message))
                return;
            // Create a number to track where the prefix ends and the command begins
            var argPos = 0;
            // Determine if the message is a command, based on if it starts with '!' or a mention prefix
            if (!(message.HasCharPrefix('-', ref argPos) || message.HasMentionPrefix(Client.CurrentUser, ref argPos)))
                return;

            // Create a Command Context
            var context = new CommandContext(Client, message);
            // Execute the command. (result does not indicate a return value,
            // rather an object stating if the command executed successfully)
            await Commands.ExecuteAsync(context, argPos, _services);
        }

        private static void SetApiKeys(IConfiguration config)
        {
            KeyStorage.ApiKey = _config.GetSection("keys:champion.gg").Value;
            SpeedrunAPIClient.ApiKey = _config.GetSection("keys:speedrun").Value;
            YoutubeRequester.ApiKey = _config.GetSection("keys:youtube").Value;
        }
    }
}