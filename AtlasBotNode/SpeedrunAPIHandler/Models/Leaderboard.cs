using Newtonsoft.Json;
using System.Collections.Generic;

namespace SpeedrunAPIHandler.Models
{
    public class RootLeaderboard
    {
        [JsonProperty("data")]
        public Leaderboard Leaderboard { get; set; }
    }

    public class Leaderboard
    {
        [JsonProperty("weblink")]
        public string WebLink { get; set; }

        [JsonProperty("runs")]
        public List<RunList> RunList { get; set; }

        [JsonProperty("game")]
        public RunGame RunGame { get; set; }

        public Game Game => RunGame?.Game;

        [JsonProperty("players")]
        public LeaderboardPlayers LeaderboardPlayers { get; set; }

        [JsonProperty("category")]
        public RunCategory Category { get; set; }

        public string GetCategoryName => Category?.Category?.Name;
        public IEnumerable<User> Players => LeaderboardPlayers?.Players;
    }

    public class LeaderboardPlayers
    {
        [JsonProperty("data")]
        public List<User> Players { get; set; }
    }

    public class RunList
    {
        [JsonProperty("place")]
        public int Place { get; set; }

        [JsonProperty("run")]
        public Run Run { get; set; }
    }
}