using AtlasBotNode.EmbedGenerators.ModuleGenerators;
using ChampionGgApiHandler;
using Discord;
using Discord.Commands;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace AtlasBotNode.Modules
{
    [Group("lol")]
    [Alias("league")]
    public class LeagueModule : ModuleBase
    {
        private readonly ChampionGgClient _championGgClient;
        private readonly ILeagueEmbedGenerator _leagueEmbedGenerator;

        public LeagueModule(ILeagueEmbedGenerator leagueEmbedGenerator)
        {
            _championGgClient = new ChampionGgClient();
            _leagueEmbedGenerator = leagueEmbedGenerator;
        }

        [Command("performance")]
        [Alias("op", "ops")]
        [Summary("Gets the latest best and worst performance for every lane from Champion.gg")]
        public async Task GetPerformance([Optional] string division)
        {
            var performance = await _championGgClient.Performance.GetDefaultPerformance();
            if (performance == null)
                await ReplyAsync("Unable to get performance");
            var response = _leagueEmbedGenerator.CreatePerformanceEmbed(performance).Build();
            await ReplyAsync("", embed: response);
        }

        [Command("champion")]
        public async Task GetChampionData(int championId)
        {
            var championData = await _championGgClient.Champions.GetChampionData(championId);
            if (championData == null)
                await ReplyAsync("Unable to get champion");
            var response = _leagueEmbedGenerator.CreateChampionDataEmbed(championData).Build();
            await ReplyAsync("", embed: response);
        }
    }
}