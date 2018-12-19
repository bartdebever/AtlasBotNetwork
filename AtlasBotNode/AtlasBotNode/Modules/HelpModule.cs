using AtlasBotNode.EmbedGenerators;
using AtlasBotNode.EmbedGenerators.ModuleGenerators;
using AtlasBotNode.Helpers;
using Discord.Commands;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AtlasBotNode.Modules.Base;

namespace AtlasBotNode.Modules
{
    [Group("Help")]
    public class HelpModule : AtlasModule
    {
        private readonly IDefaultEmbedGenerator _defaultEmbedGenerator;
        private readonly IHelpEmbedGenerator _helpEmbedGenerator;

        public HelpModule(IDefaultEmbedGenerator defaultEmbedGenerator, IHelpEmbedGenerator helpEmbedGenerator)
        {
            _defaultEmbedGenerator = defaultEmbedGenerator;
            _helpEmbedGenerator = helpEmbedGenerator;
        }
        
        public override string Identifier => "Help";

        [Command("")]
        [Summary("Gives you the list of commands available.")]
        public async Task HelpList()
        {
            var embed = _helpEmbedGenerator.GenerateHelpListEmbed(DiscordCommandHelper.CommandService.Modules).Build();

            await ReplyAsync("", embed: embed);
        }
    }
}