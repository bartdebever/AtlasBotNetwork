using System.Collections.Generic;
using Newtonsoft.Json;
using SpeedrunAPIHandler.Models.Categories;
using SpeedrunAPIHandler.Models.Games;

namespace SpeedrunAPIHandler.Models.Leaderboards
{
    public class Leaderboard
    {
        /// <summary>
        /// Gets or sets the link to the run on the speedrun.com website.
        /// </summary>
        [JsonProperty("weblink")]
        public string WebLink { get; set; }

        /// <summary>
        /// Gets or sets the list of runs.
        /// </summary>
        [JsonProperty("runs")]
        public List<RunList> RunList { get; set; }

        /// <summary>
        /// Gets or sets the game that is ran.
        /// Please use <see cref="Game"/> instead.
        /// </summary>
        [JsonProperty("game")]
        public RunGame RunGame { get; set; }

        /// <summary>
        /// Gets the game that is being ran.
        /// </summary>
        public Game Game => RunGame?.Game;

        /// <summary>
        /// Gets or sets the players participating in this leaderboard.
        /// Please use <see cref="Players"/> instead.
        /// </summary>
        [JsonProperty("players")]
        public LeaderboardPlayers LeaderboardPlayers { get; set; }

        /// <summary>
        /// Gets or sets the category of the leaderboard.
        /// </summary>
        [JsonProperty("category")]
        public RunCategory Category { get; set; }

        /// <summary>
        /// Gets the name of the category.
        /// </summary>
        public string GetCategoryName => Category?.Category?.Name;
        
        /// <summary>
        /// Gets the players participating in this leaderboard.
        /// </summary>
        public IEnumerable<User> Players => LeaderboardPlayers?.Players;
    }
}