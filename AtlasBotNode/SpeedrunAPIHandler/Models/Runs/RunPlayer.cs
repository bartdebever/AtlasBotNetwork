using Newtonsoft.Json;

namespace SpeedrunAPIHandler.Models.Runs
{
    public class RunPlayer
    {
        [JsonProperty("id")] public string Id { get; set; }
    }
}