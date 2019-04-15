using Newtonsoft.Json;

namespace SpeedrunAPIHandler.Models.Runs
{
    public class Times
    {
        [JsonProperty("primary")] public string Primary { get; set; }
    }
}