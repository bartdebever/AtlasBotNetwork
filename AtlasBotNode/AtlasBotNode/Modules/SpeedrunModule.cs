using AtlasBotNode.EmbedGenerators;
using AtlasBotNode.EmbedGenerators.ModuleGenerators;
using Discord;
using Discord.Commands;
using SpeedrunAPIHandler;
using System.Threading.Tasks;
using AtlasBotNode.Modules.Base;

namespace AtlasBotNode.Modules
{
    [Group("Speedrun")]
    [Alias("SR")]
    public class SpeedrunModule : AtlasModule
    {
        private readonly SpeedrunAPIClient _speedrunApiClient;
        private readonly ISpeedrunEmbedGenerator _speedrunEmbedGenerator;
        private readonly IDefaultEmbedGenerator _defaultEmbedGenerator;

        public SpeedrunModule(ISpeedrunEmbedGenerator speedrunEmbedGenerator, IDefaultEmbedGenerator defaultEmbedGenerator)
        {
            _defaultEmbedGenerator = defaultEmbedGenerator;
            _speedrunEmbedGenerator = speedrunEmbedGenerator;
            _speedrunApiClient = new SpeedrunAPIClient();
        }
        
        public override string Identifier => "Speedrun";

        [Command("leaderboard")]
        [Alias("Ranking", "LB")]
        public async Task GetLeaderboard(string game, string category)
        {
            Embed response;
            try
            {
                var leaderboard = await _speedrunApiClient.LeaderboardModule.GetLeaderboard(game, category.Replace("%", ""));
                response = _speedrunEmbedGenerator.CreateLeaderboardEmbed(leaderboard)
                    .Build();
            }
            catch
            {
                response = _defaultEmbedGenerator.GenerateNotFoundEmbed("speedrun", "leaderboard", "Can't find game or Category",
                    "Game or Category was not found, sorry!\nThe Speedrun.com API can be a little specific, there will be a guide on how to get everything right soon.")
                    .Build();
            }

            await ReplyAsync("", embed: response);
        }

        [Command("worldrecord")]
        [Alias("wr")]
        public async Task GetWorldRecord(string game, string category)
        {
            Embed response;
            try
            {
                var leaderboard =
                    await _speedrunApiClient.LeaderboardModule.GetLeaderboard(game, category.Replace("%", ""), 1);
                response = _speedrunEmbedGenerator.CreateWorldRecordEmbed(leaderboard).Build();
            }
            catch
            {
                response = _defaultEmbedGenerator.GenerateNotFoundEmbed("speedrun", "worldrecord", "Can't find game or Category",
                    "Game or Category was not found, sorry!\nThe Speedrun.com API can be a little specific, there will be a guide on how to get everything right soon.")
                    .Build();
            }

            await ReplyAsync("", embed: response);
        }
    }
}