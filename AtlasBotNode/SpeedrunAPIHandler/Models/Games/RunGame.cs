using Newtonsoft.Json;

namespace SpeedrunAPIHandler.Models.Games
{
    public class RunGame
    {
        [JsonProperty("data")]
        public Game Game { get; set; }
    }
}