using Newtonsoft.Json;

namespace SpeedrunAPIHandler.Models
{
    public class RunCategory
    {
        [JsonProperty("data")]
        public Category Category { get; set; }
    }

    public class Category
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}