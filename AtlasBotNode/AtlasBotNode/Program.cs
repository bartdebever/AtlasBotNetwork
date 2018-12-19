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
using AtlasBotNode.Communication;
using AtlasBotNode.Configuration;
using AtlasBotNode.Configuration.Models;
using AtlasBotNode.Modules;
using ChampionGgApiHandler;
using Microsoft.Extensions.Configuration;
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

        private async Task InstallCommands()
        {
            // Hook the MessageReceived Event into our Command Handler
            _client.MessageReceived += HandleCommand;
            //Add all modules specified in the config file.
            await AddModules();
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

        private static void SetApiKeys(KeyConfiguration config)
        {
            KeyStorage.ApiKey = config.Championgg;
            SpeedrunAPIClient.ApiKey = config.Speedrun;
            YoutubeRequester.ApiKey = config.YouTube;
        }

        private void SetupCommander()
        {
            var commanderSection = _configuration.CommanderConfiguration;
            var commanderConnector = new CommanderConnector(commanderSection.IpAddress,
                commanderSection.Port, commanderSection.NodeName,
                commanderSection.CommanderToken);
            commanderConnector.Connect();
            commanderConnector.Register();
            _client.Log += commanderConnector.LogDiscord;
            _commands.Log += commanderConnector.LogDiscord;
        }
        
        private async Task AddModules()
        {
            // TODO you are next to die, switch statement
            foreach (var child in _configuration.Modules)
            {
                switch (child)
                {
                    case "All":
                        await _commands.AddModulesAsync(Assembly.GetEntryAssembly());
                        return;
                    case "Smash4":
                        await _commands.AddModuleAsync<Smash4Module>();
                        break;
                    case "Speedrun":
                        await _commands.AddModuleAsync<SpeedrunModule>();
                        break;
                    case "Help":
                        await _commands.AddModuleAsync<HelpModule>();
                        break;
                    case "Role":
                        await _commands.AddModuleAsync<RoleModule>();
                        break;
                    case "Smashgg":
                        await _commands.AddModuleAsync<SmashggModule>();
                        break;
                    case "Youtube":
                        await _commands.AddModuleAsync<YoutubeModule>();
                        break;
                    case "Quiz":
                        await _commands.AddModuleAsync<QuizModule>();
                        break;
                    case "Test":
                        await _commands.AddModuleAsync<TestModule>();
                        break;
                }
            }
        }
    }
}