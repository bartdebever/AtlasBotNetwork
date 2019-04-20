using System.Threading.Tasks;
using AtlasBotNode.EmbedGenerators;
using AtlasBotNode.EmbedGenerators.ModuleGenerators.Interfaces;
using AtlasBotNode.Helpers;
using Discord.Commands;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AtlasBotNode.Configuration;

namespace AtlasBotNode.Modules.Help
{
    [Group("Help")]
    public class HelpModule : ModuleBase
    {
        private readonly IDefaultEmbedGenerator _defaultEmbedGenerator;
        private readonly IHelpEmbedGenerator _helpEmbedGenerator;

        public HelpModule(IDefaultEmbedGenerator defaultEmbedGenerator, IHelpEmbedGenerator helpEmbedGenerator)
        {
            _defaultEmbedGenerator = defaultEmbedGenerator;
            _helpEmbedGenerator = helpEmbedGenerator;
        }

        [Command("")]
        [Summary("Gives you the list of commands available.")]
        public async Task HelpList()
        {
            var embed = _helpEmbedGenerator.GenerateHelpListEmbed(DiscordCommandConfiguration.CommandService.Modules).Build();

            await ReplyAsync(string.Empty, embed: embed);
        }
    }
}