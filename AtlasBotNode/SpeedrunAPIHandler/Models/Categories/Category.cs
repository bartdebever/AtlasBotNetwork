using Newtonsoft.Json;

namespace SpeedrunAPIHandler.Models.Categories
{
    public class Category
    {
        /// <summary>
        ///     Gets or sets the name of the category.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}