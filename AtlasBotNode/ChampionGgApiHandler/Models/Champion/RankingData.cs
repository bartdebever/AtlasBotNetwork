using Newtonsoft.Json;

namespace ChampionGgApiHandler.Models.Champion
{
    public class RankingData
    {
        [JsonProperty("kills")]
        public int Kills { get; set; }

        [JsonProperty("deaths")]
        public int Deaths { get; set; }

        [JsonProperty("assists")]
        public int Assists { get; set; }

        [JsonProperty("winRates")]
        public int WinRates { get; set; }

        [JsonProperty("banRates")]
        public int BanRates { get; set; }

        [JsonProperty("playRates")]
        public int PickRates { get; set; }

        [JsonProperty("damageDealt")]
        public int DamageDealt { get; set; }
    }
}