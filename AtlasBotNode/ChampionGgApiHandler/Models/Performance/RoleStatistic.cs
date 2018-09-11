using Newtonsoft.Json;

namespace ChampionGgApiHandler.Models.Performance
{
    public class RoleStatistic
    {
        [JsonProperty("performanceDelta")]
        public Statistic PerformanceDelta { get; set; }

        [JsonProperty("winrate")]
        public Statistic WinRate { get; set; }

        [JsonProperty("performanceScore")]
        public Statistic PerformanceScore { get; set; }
    }
}