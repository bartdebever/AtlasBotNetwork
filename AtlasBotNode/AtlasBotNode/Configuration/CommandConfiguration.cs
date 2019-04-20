using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AtlasBotNode.Modules;
using Microsoft.Extensions.Configuration;

namespace AtlasBotNode.Configuration
{
    public class CommandConfiguration
    {
        public static async Task<IServiceProvider> AddModules(IConfiguration config, IServiceProvider services)
        {
            var commands = DiscordCommandConfiguration.CommandService;
            foreach (var child in config.GetChildren())
            {
                switch (child.Value)
                {
                    case "All":
                        await commands.AddModulesAsync(Assembly.GetEntryAssembly(), services);
                        return services;
                    case "Smash4":
                        await commands.AddModuleAsync<Smash4Module>(services);
                        break;
                    case "Speedrun":
                        await commands.AddModuleAsync<SpeedrunModule>(services);
                        break;
                    case "Help":
                        await commands.AddModuleAsync<HelpModule>(services);
                        break;
                    case "Role":
                        await commands.AddModuleAsync<RoleModule>(services);
                        break;
                    case "Smashgg":
                        await commands.AddModuleAsync<SmashggModule>(services);
                        break;
                    case "Youtube":
                        await commands.AddModuleAsync<YoutubeModule>(services);
                        break;
                    case "Quiz":
                        await commands.AddModuleAsync<QuizModule>(services);
                        break;
                    case "Test":
                        await commands.AddModuleAsync<TestModule>(services);
                        break;
                }
            }

            return services;
        }
    }
}
