using AtlasBotNode.Helpers;
using Discord;
using SpeedrunAPIHandler.Models;
using System;
using System.Linq;
using System.Text;
using AtlasBotNode.EmbedGenerators.ModuleGenerators.Interfaces;
using SpeedrunAPIHandler.Models.Leaderboards;

namespace AtlasBotNode.EmbedGenerators.ModuleGenerators
{
    public class SpeedrunEmbedGenerator : ISpeedrunEmbedGenerator
    {
        private EmbedBuilder _embedBuilder;

        private void ResetEmbedBuilder()
        {
            _embedBuilder = new EmbedBuilder();
            _embedBuilder.WithColor(Color.Red);
            _embedBuilder.WithCurrentTimestamp();
            _embedBuilder.WithFooter("Speedrun.com API");
        }

        public ISpeedrunEmbedGenerator CreateLeaderboardEmbed(Leaderboard leaderboard)
        {
            ResetEmbedBuilder();

            _embedBuilder.WithTitle($"{leaderboard.Game.Names["international"]} {leaderboard.GetCategoryName} Leaderboard");
            _embedBuilder.WithUrl(leaderboard.WebLink);
            var stringBuilder = new StringBuilder();
            foreach (var run in leaderboard.RunList)
            {
                var player = leaderboard.Players.FirstOrDefault(x => x.Id == run.Run.Players[0]?.Id);
                stringBuilder.AppendLine($"**{StringCleanerHelper.NumberToRanking(run.Place)}**: {player?.Names["international"]} {StringCleanerHelper.SpeedrunTimeConverter(run.Run.Time.Primary)}");
            }
            _embedBuilder.AddField("Runs", stringBuilder);

            var icon = leaderboard.Game.Assets["icon"];
            if (icon != null)
                _embedBuilder.WithThumbnailUrl(icon.Uri);
            return this;
        }

        public ISpeedrunEmbedGenerator CreateWorldRecordEmbed(Leaderboard leaderboard)
        {
            ResetEmbedBuilder();

            var run = leaderboard.RunList[0];
            var runner = leaderboard.Players.FirstOrDefault(x => x.Id == run.Run.Players[0].Id);
            if (run == null || runner == null)
                throw new Exception();
            _embedBuilder.WithTitle(
                $"{leaderboard.Game.Names["international"]} {leaderboard.GetCategoryName} World Record");
            _embedBuilder.WithUrl(leaderboard.WebLink);
            _embedBuilder.AddField("World Record",
                $"**{runner.Names["international"]}:** {StringCleanerHelper.SpeedrunTimeConverter(run.Run.Time.Primary)}\n" +
                $"Check out the run at: {run.Run.WebLink}"
            );

            if (leaderboard.Game.Assets.ContainsKey("cover-medium"))
                _embedBuilder.WithThumbnailUrl(leaderboard.Game.Assets["cover-medium"].Uri);

            if (leaderboard.Game.Assets.ContainsKey("icon"))
                _embedBuilder.WithImageUrl(leaderboard.Game.Assets["icon"].Uri);

            return this;
        }

        public Embed Build()
        {
            return _embedBuilder.Build();
        }
    }
}