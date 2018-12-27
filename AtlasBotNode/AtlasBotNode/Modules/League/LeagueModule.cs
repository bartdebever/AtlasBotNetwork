using System.Runtime.InteropServices;
using System.Threading.Tasks;
using AtlasBotNode.EmbedGenerators.ModuleGenerators.Interfaces;
using AtlasBotNode.Helpers;
using ChampionGgApiHandler;
using Discord.Commands;
using LoLHandler;

namespace AtlasBotNode.Modules.League
{
    [Group("lol")]
    [Alias("league")]
    public class LeagueModule : ModuleBase
    {
        private readonly ChampionGgClient _championGgClient;
        private readonly LoLClient _lolClient;
        private readonly ILeagueEmbedGenerator _leagueEmbedGenerator;

        public LeagueModule(ILeagueEmbedGenerator leagueEmbedGenerator)
        {
            _championGgClient = new ChampionGgClient();
            _leagueEmbedGenerator = leagueEmbedGenerator;
            _lolClient = new LoLClient();
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
        public async Task GetChampion(string championName)
        {
            var champion = LoLChampionHelper.GetChampionByName(championName);
            var championDto = await _lolClient.Champions.GetChampionByNameAsync(champion.InternalName);
            var embed = _leagueEmbedGenerator.CreateChampionEmbed(championDto, champion.InternalName).Build();
            await ReplyAsync("", embed: embed);
        }

        [Command("spells")]
        public async Task GetChampionSpells(string championName)
        {
            var champion = LoLChampionHelper.GetChampionByName(championName);
            if (champion == null)
            {
                await ReplyAsync("Can not find a champion by that name");
                return;
            }

            var championDto = await _lolClient.Champions.GetChampionByNameAsync(champion.InternalName);
            var response = _leagueEmbedGenerator.CreateChampionSpellsEmbed(championDto, champion).Build();
            await ReplyAsync("", embed: response);
        }

        [Command("stats")]
        public async Task GetStats(int championId)
        {
            var championData = await _championGgClient.Champions.GetChampionStats(championId);
            if (championData == null)
                await ReplyAsync("Unable to get that champion");
            var response = _leagueEmbedGenerator.CreateChampionDataEmbed(championData).Build();
            await ReplyAsync("", embed: response);
        }

        [Command("build")]
        public async Task GetChampionBuild([Remainder] string champion)
        {
            var championDto = LoLChampionHelper.GetChampionByName(champion);
            var championData = await _championGgClient.Champions.GetChampionStats(championDto.Id);
            if(championData == null)
                await ReplyAsync("Unable to get that champion");
            var response = _leagueEmbedGenerator.CreateChampionBuildEmbed(championData, championDto).Build();
            await ReplyAsync("", embed: response);
        }
    }
}