﻿using Newtonsoft.Json;

namespace ChampionGgApiHandler.Models.Champion
{
    public class ChampionData
    {
        [JsonProperty("elo")]
        public string Elo { get; set; }

        [JsonProperty("patch")]
        public string Patch { get; set; }

        [JsonProperty("championId")]
        public int ChampionId { get; set; }

        [JsonProperty("winRate")]
        public double WinRate { get; set; }

        [JsonProperty("playRate")]
        public double PickRate { get; set; }

        [JsonProperty("banRate")]
        public double BanRate { get; set; }

        [JsonProperty("kills")]
        public double Kills { get; set; }

        [JsonProperty("deaths")]
        public double Deaths { get; set; }

        [JsonProperty("assists")]
        public double Assists { get; set; }

        public double Kda => Kills + Assists / Deaths;

        [JsonProperty("gamesPlayed")]
        public int GamesPlayed { get; set; }

        [JsonProperty("role")]
        public string Role { get; set; }

        [JsonProperty("damageComposition")]
        public DamageData Damage { get; set; }

        [JsonProperty("positions")]
        public RankingData Rankings { get; set; }
    }
}