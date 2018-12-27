using Newtonsoft.Json;

namespace SpeedrunAPIHandler.Models.Categories
{
    public class Category
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}