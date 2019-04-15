using Newtonsoft.Json;

namespace SpeedrunAPIHandler.Models
{
    public class Image
    {
        [JsonProperty("uri")] public string Uri { get; set; }
    }
}