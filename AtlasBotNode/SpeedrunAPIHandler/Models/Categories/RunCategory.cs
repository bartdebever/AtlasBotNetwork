using Newtonsoft.Json;

namespace SpeedrunAPIHandler.Models.Categories
{
    public class RunCategory
    {
        /// <summary>
        ///     Gets or sets the category.
        /// </summary>
        [JsonProperty("data")]
        public Category Category { get; set; }
    }
}