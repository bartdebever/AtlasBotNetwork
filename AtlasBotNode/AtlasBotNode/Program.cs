﻿using AtlasBotNode.EmbedGenerators;
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
        private CommandService _commands;
        private DiscordSocketClient _client;
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

            var section = _config.GetSection("keys");
            _client = new DiscordSocketClient();
            _commands = new CommandService();
            SetApiKeys(section);
            DiscordCommandHelper.CommandService = _commands;
            DiscordCommandHelper.Client = _client;

            _client.Log += DefaultLogger.Logger;
            _commands.Log += DefaultLogger.Logger;

            _services = new ServiceCollection()
                .AddEmbedGenerators()
                .BuildServiceProvider();

            await InstallCommands();


            await _client.LoginAsync(TokenType.Bot, section.GetSection("discord").Value);
            await _client.StartAsync();

            await Task.Delay(-1);
        }

        private async Task InstallCommands()
        {
            // Hook the MessageReceived Event into our Command Handler
            _client.MessageReceived += HandleCommand;
            //Add all modules specified in the config file.
            await AddModules(_config.GetSection("modules"));
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

        private static void SetApiKeys(IConfiguration config)
        {
            KeyStorage.ApiKey = config.GetSection("champion.gg").Value;
            SpeedrunAPIClient.ApiKey = config.GetSection("speedrun").Value;
            YoutubeRequester.ApiKey = config.GetSection("youtube").Value;
        }

        private async Task AddModules(IConfiguration config)
        {
            foreach (var child in config.GetChildren())
            {
                switch (child.Value)
                {
                    case "All":
                        await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
                        return;
                    case "Smash4":
                        await _commands.AddModuleAsync<Smash4Module>(_services);
                        break;
                    case "Speedrun":
                        await _commands.AddModuleAsync<SpeedrunModule>(_services);
                        break;
                    case "Help":
                        await _commands.AddModuleAsync<HelpModule>(_services);
                        break;
                    case "Role":
                        await _commands.AddModuleAsync<RoleModule>(_services);
                        break;
                    case "Smashgg":
                        await _commands.AddModuleAsync<SmashggModule>(_services);
                        break;
                    case "Youtube":
                        await _commands.AddModuleAsync<YoutubeModule>(_services);
                        break;
                    case "Quiz":
                        await _commands.AddModuleAsync<QuizModule>(_services);
                        break;
                    case "Test":
                        await _commands.AddModuleAsync<TestModule>(_services);
                        break;
                }
            }
        }
    }
}