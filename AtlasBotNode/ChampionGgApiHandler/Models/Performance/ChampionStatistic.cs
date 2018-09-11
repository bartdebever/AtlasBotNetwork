using Newtonsoft.Json;

namespace ChampionGgApiHandler.Models.Performance
{
    public class ChampionStatistic
    {
        [JsonProperty("score")]
        public double Score { get; set; }

        [JsonProperty("championId")]
        public int ChampionId { get; set; }
    }
}