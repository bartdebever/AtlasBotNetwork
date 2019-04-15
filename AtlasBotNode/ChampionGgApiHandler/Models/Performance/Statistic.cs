using Newtonsoft.Json;

namespace ChampionGgApiHandler.Models.Performance
{
    public class Statistic
    {
        [JsonProperty("best")] public ChampionStatistic Best { get; set; }

        [JsonProperty("worst")] public ChampionStatistic Worst { get; set; }
    }
}