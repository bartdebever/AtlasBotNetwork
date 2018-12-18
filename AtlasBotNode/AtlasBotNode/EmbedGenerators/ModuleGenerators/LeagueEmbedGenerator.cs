using System.Globalization;
using AtlasBotNode.Helpers;
using ChampionGgApiHandler.Models.Champion;
using ChampionGgApiHandler.Models.Performance;
using Discord;
using System.Text;
using RestSharp.Extensions;

namespace AtlasBotNode.EmbedGenerators.ModuleGenerators
{
    public interface ILeagueEmbedGenerator : IEmbedGenerator
    {
        ILeagueEmbedGenerator CreatePerformanceEmbed(Performance performance);
        ILeagueEmbedGenerator CreateChampionSpellsEmbed(LoLHandler.Dtos.ChampionDto champion, ChampionDto championDto);
        ILeagueEmbedGenerator CreateChampionEmbed(LoLHandler.Dtos.ChampionDto champion, string internalName);
        ILeagueEmbedGenerator CreateChampionDataEmbed(ChampionData championData);
        ILeagueEmbedGenerator CreateChampionBuildEmbed(ChampionData championData, ChampionDto championDto);
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

        public ILeagueEmbedGenerator CreateChampionSpellsEmbed(LoLHandler.Dtos.ChampionDto champion, ChampionDto championDto)
        {
            ResetBuilder();

            _embedBuilder.WithThumbnailUrl(
                $"http://ddragon.leagueoflegends.com/cdn/8.19.1/img/champion/{championDto.InternalName}.png");

//            Didn't enjoy the look of this at all
//            var infoStringBuilder = new StringBuilder();
//            foreach (var info in champion.Info)
//            {
//                infoStringBuilder.AppendLine($"{info.Key.ToPascalCase(CultureInfo.CurrentCulture)}: {info.Value}");
//            }
//            _embedBuilder.AddField("Statistics", infoStringBuilder.ToString());

            var spellStringBuilder = new StringBuilder();
            _embedBuilder.AddField("Passive", $"**{champion.Passive.Name}**\n{champion.Passive.Description.Replace("<br>", "\n")}");
            var keys = new string[]{"Q", "W", "E", "R"};
            for (var i = 0; i < 4; i++)
            {
                var spell = champion.Spells[i];
                spellStringBuilder.Clear();
                spellStringBuilder.AppendLine($"**{spell.Name}**");
                spellStringBuilder.AppendLine($"{spell.Description}");
                if (!string.IsNullOrWhiteSpace(spell.CooldownBurn) || spell.CooldownBurn.Equals("0"))
                    spellStringBuilder.AppendLine($"**Cooldown:** {spell.CooldownBurn}");
                _embedBuilder.AddField(keys[i], spellStringBuilder.ToString());
            }



            return this;
        }

        public ILeagueEmbedGenerator CreateChampionEmbed(LoLHandler.Dtos.ChampionDto champion, string internalName)
        {
            ResetBuilder();
            _embedBuilder.WithImageUrl($"http://ddragon.leagueoflegends.com/cdn/img/champion/splash/{internalName}_0.jpg");
            _embedBuilder.AddField(champion.Name, champion.Title);
            _embedBuilder.AddField("Commands", $"User \"-lol spells {champion.Name}\" to get details on what spells {champion.Name} has.");

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

        public ILeagueEmbedGenerator CreateChampionBuildEmbed(ChampionData championData, ChampionDto championDto)
        {
            ResetBuilder();

            _embedBuilder.WithTitle($"{championDto.Name} for patch {championData.Patch}");

            CreateFieldForHash(championData.Hashes.FirstItems, "First Items", true);
            CreateFieldForHash(championData.Hashes.Items, "Final Build", true);
            CreateFieldForHash(championData.Hashes.Skills, "Skill Order");
            CreateFieldForHash(championData.Hashes.Runes, "Runes", true);
            CreateFieldForHash(championData.Hashes.Summoners, "Summoners", true);

            _embedBuilder.WithThumbnailUrl($"http://ddragon.leagueoflegends.com/cdn/8.19.1/img/champion/{championDto.InternalName}.png");
            return this;
        }

        private void CreateFieldForHash(HashCategory hash, string title, bool useEmoji = false)
        {
            if (hash == null)
                return;
            var used = hash.MostUsed;
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"Wins: {used.Wins}");
            stringBuilder.AppendLine($"Total Used: {used.Count}");
            var hashStringBuilder = new StringBuilder();
            var ids = used.Hash.Split('-');
            if (useEmoji)
            {

                foreach (var id in ids)
                {
                    hashStringBuilder.Append($"{EmojiGetterHelper.GetEmoji(id)} ");
                }
            }
            else
            {
                foreach (var id in ids)
                {
                    hashStringBuilder.Append($"{id} ");
                }
            }

            stringBuilder.AppendLine(hashStringBuilder.ToString());
            _embedBuilder.AddField(title, stringBuilder.ToString());

        }
    }
}