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
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AtlasBotNode.Communication;
using AtlasBotNode.Configuration;
using AtlasBotNode.Configuration.Models;
using ChampionGgApiHandler;
using Microsoft.Extensions.Configuration;

using SmashggNet;

using SpeedrunAPIHandler;
using YoutubeApiHandler;

namespace AtlasBotNode
{
    public class Program
    {
        private CommandService _commands;
        private DiscordSocketClient _client;
        private IServiceProvider _services;
        private static BaseConfiguration _configuration;

        private static void Main(string[] args)
        {
            _configuration = new ConfigurationLoader().CreateConfiguration("appsettings.json");
            new Program().Start().GetAwaiter().GetResult();
        }

        /// <summary>
        /// Start the Discord Bot process.
        /// </summary>
        /// <returns>An awaitable task.</returns>
        private async Task Start()
        {
            _client = new DiscordSocketClient();
            _commands = new CommandService();

            SetApiKeys(_configuration.KeyConfiguration);

            DiscordCommandHelper.CommandService = _commands;
            DiscordCommandHelper.Client = _client;

            if (_configuration.CommanderConfiguration.UseCommander)
            {
                SetupCommander();
            }
            else
            {
                _client.Log += DefaultLogger.Logger;
                _commands.Log += DefaultLogger.Logger;
            }

            _services = DependencyInjection.GetServiceCollection().BuildServiceProvider();

            await InstallCommands();

            await _client.LoginAsync(TokenType.Bot, _configuration.KeyConfiguration.Discord);
            await _client.StartAsync();

            await Task.Delay(-1);
        }

        /// <summary>
        /// Installs the commands into Discord.net and allows them to run.
        /// </summary>
        /// <returns>An awaitable task.</returns>
        private async Task InstallCommands()
        {
            // Hook the MessageReceived Event into our Command Handler
            _client.MessageReceived += HandleCommand;

            //Add all modules specified in the config file.
            var moduleLoader = new ModuleLoader();
            var modules = moduleLoader.GetModules(_configuration.Modules);
            foreach (var module in modules)
            {
                await _commands.AddModuleAsync(module);
            }
        }

        private async Task HandleCommand(SocketMessage messageParam)
        {
            // Don't process the command if it was a System Message
            if (!(messageParam is SocketUserMessage message))
                return;
            // Create a number to track where the prefix ends and the command begins
            var argPos = 0;
            // Determine if the message is a command, based on if it starts with '!' or a mention prefix
            if (!(message.HasCharPrefix('-', ref argPos) || message.HasMentionPrefix(_client.CurrentUser, ref argPos)))
                return;

            // Create a Command Context
            var context = new CommandContext(_client, message);
            // Execute the command. (result does not indicate a return value,
            // rather an object stating if the command executed successfully)
            await _commands.ExecuteAsync(context, argPos, _services);
        }

        /// <summary>
        /// Sets up the API keys for the API wrappers to run from.
        /// </summary>
        /// <param name="config">The config section that includes all the API keys.</param>
        private static void SetApiKeys(KeyConfiguration config)
        {
            KeyStorage.ApiKey = config.Championgg;
            SpeedrunApiClient.ApiKey = config.Speedrun;
            YoutubeClient.ApiKey = config.YouTube;
            SmashggClient.ApiToken = config.Smashgg;
        }

        /// <summary>
        /// Sets up the commander configuration.
        /// </summary>
        private void SetupCommander()
        {
            var commanderSection = _configuration.CommanderConfiguration;
            var commanderConnector = new CommanderConnector(
                commanderSection.IpAddress,
                commanderSection.Port,
                commanderSection.NodeName,
                commanderSection.CommanderToken);
            commanderConnector.Connect();
            commanderConnector.Register();
            _client.Log += commanderConnector.LogDiscord;
            _commands.Log += commanderConnector.LogDiscord;
        }
    }
}