using Newtonsoft.Json;

namespace SpeedrunAPIHandler.Models.Leaderboards
{
    public class RootLeaderboard
    {
        [JsonProperty("data")]
        public Leaderboard Leaderboard { get; set; }
    }
}