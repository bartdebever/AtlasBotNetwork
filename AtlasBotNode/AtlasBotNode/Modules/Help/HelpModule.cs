namespace AtlasBotNode.Modules.Help
{
    using System.Threading.Tasks;
    using Discord.Commands;
    using EmbedGenerators;
    using EmbedGenerators.ModuleGenerators.Interfaces;

    using Helpers;

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
            var embed = _helpEmbedGenerator.GenerateHelpListEmbed(DiscordCommandHelper.CommandService.Modules).Build();

            await ReplyAsync(string.Empty, embed: embed);
        }
    }
}