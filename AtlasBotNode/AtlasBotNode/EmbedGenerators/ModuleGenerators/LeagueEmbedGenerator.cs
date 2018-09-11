using AtlasBotNode.Helpers;
using ChampionGgApiHandler.Models.Champion;
using ChampionGgApiHandler.Models.Performance;
using Discord;
using System.Text;

namespace AtlasBotNode.EmbedGenerators.ModuleGenerators
{
    public interface ILeagueEmbedGenerator : IEmbedGenerator
    {
        ILeagueEmbedGenerator CreatePerformanceEmbed(Performance performance);

        ILeagueEmbedGenerator CreateChampionDataEmbed(ChampionData championData);
    }

    public class LeagueEmbedGenerator : ILeagueEmbedGenerator
    {
        private EmbedBuilder _embedBuilder;

        private void ResetBuilder()
        {
            _embedBuilder = new EmbedBuilder();
            _embedBuilder.WithColor(Color.DarkBlue);
            _embedBuilder.WithCurrentTimestamp();
            _embedBuilder.WithFooter("League of Legends Module");
        }

        public Embed Build()
        {
            return _embedBuilder.Build();
        }

        public ILeagueEmbedGenerator CreatePerformanceEmbed(Performance performance)
        {
            ResetBuilder();

            _embedBuilder.WithTitle($"Performances for patch {performance.Patch}");

            foreach (var roleStatistic in performance.RoleStatistics)
            {
                var statistic = roleStatistic.Value;
                var stringBuilder = new StringBuilder();

                stringBuilder.AppendLine("**Win-Rate**");
                stringBuilder.AppendLine($"**Best:** {statistic.WinRate.Best.ChampionId.ToEmoji()} {statistic.WinRate.Best.Score.ToPercentage()}");
                stringBuilder.AppendLine($"**Worst:** {statistic.WinRate.Worst.ChampionId.ToEmoji()} {statistic.WinRate.Worst.Score.ToPercentage()}");

                stringBuilder.AppendLine("**Over-all Performance**");
                stringBuilder.AppendLine($"**Best:** {statistic.PerformanceScore.Best.ChampionId.ToEmoji()} {statistic.PerformanceScore.Best.Score.ToChampionGgScore()}");
                stringBuilder.AppendLine($"**Worst:** {statistic.PerformanceScore.Worst.ChampionId.ToEmoji()} {statistic.PerformanceScore.Worst.Score.ToChampionGgScore()}");

                _embedBuilder.AddField(StringCleanerHelper.ChampionGgRoleName(roleStatistic.Key), stringBuilder.ToString(), true);
            }

            return this;
        }

        public ILeagueEmbedGenerator CreateChampionDataEmbed(ChampionData championData)
        {
            ResetBuilder();

            _embedBuilder.WithTitle($"{championData.ChampionId} as of patch {championData.Patch}");

            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"**Pick Rate:** {championData.PickRate.ToPercentage()} ({StringCleanerHelper.NumberToRanking(championData.Rankings.PickRates)})");
            stringBuilder.AppendLine($"**Win Rate:** {championData.WinRate.ToPercentage()} ({StringCleanerHelper.NumberToRanking(championData.Rankings.WinRates)})");
            stringBuilder.AppendLine($"**Ban Rate:** {championData.BanRate.ToPercentage()} ({StringCleanerHelper.NumberToRanking(championData.Rankings.BanRates)})");

            _embedBuilder.AddField("Picks/Bans", stringBuilder.ToString());

            stringBuilder.Clear();

            stringBuilder.AppendLine($"**Kills:** {championData.Kills.ToChampionGgScore()} ({StringCleanerHelper.NumberToRanking(championData.Rankings.Kills)})");
            stringBuilder.AppendLine($"**Deaths:** {championData.Deaths.ToChampionGgScore()} ({StringCleanerHelper.NumberToRanking(championData.Rankings.Deaths)})");
            stringBuilder.AppendLine($"**Assists:** {championData.Assists.ToChampionGgScore()} ({StringCleanerHelper.NumberToRanking(championData.Rankings.Assists)})");
            stringBuilder.AppendLine($"**KDA:** {championData.Kda.ToChampionGgScore()}");

            _embedBuilder.AddField("KDA", stringBuilder.ToString());

            stringBuilder.Clear();

            stringBuilder.AppendLine($"**Damage done:** {championData.Damage.Total.ToChampionGgScore()} ({StringCleanerHelper.NumberToRanking(championData.Rankings.DamageDealt)})");
            stringBuilder.AppendLine($"**True Damage:** {championData.Damage.TotalTrue.ToChampionGgScore()} ({championData.Damage.PercentageTrue.ToPercentage()})");
            stringBuilder.AppendLine($"**Magic Damage:** {championData.Damage.TotalMagic.ToChampionGgScore()} ({championData.Damage.PercentageMagic.ToPercentage()})");
            stringBuilder.AppendLine($"**Physical Damage:** {championData.Damage.TotalPhysical.ToChampionGgScore()} ({championData.Damage.PercentagePhysical.ToPercentage()})");

            _embedBuilder.AddField("Damage", stringBuilder.ToString());

            return this;
        }
    }
}