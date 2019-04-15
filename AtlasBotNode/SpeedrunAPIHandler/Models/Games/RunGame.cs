using Newtonsoft.Json;

namespace SpeedrunAPIHandler.Models.Games
{
    public class RunGame
    {
        /// <summary>
        ///     Gets or sets the game object.
        /// </summary>
        [JsonProperty("data")]
        public Game Game { get; set; }
    }
}