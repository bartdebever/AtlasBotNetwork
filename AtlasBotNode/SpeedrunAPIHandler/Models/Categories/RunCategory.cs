using Newtonsoft.Json;

namespace SpeedrunAPIHandler.Models.Categories
{
    public class RunCategory
    {
        [JsonProperty("data")]
        public Category Category { get; set; }
    }
}