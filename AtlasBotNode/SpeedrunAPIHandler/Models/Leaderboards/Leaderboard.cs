using System.Collections.Generic;
using Newtonsoft.Json;
using SpeedrunAPIHandler.Models.Categories;
using SpeedrunAPIHandler.Models.Games;

namespace SpeedrunAPIHandler.Models.Leaderboards
{
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
}