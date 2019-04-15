using Newtonsoft.Json;

namespace KhApiHandler.Models
{
    public class Character
    {
        [JsonProperty("id")] public int Id { get; set; }

        [JsonProperty("style")] public string Style { get; set; }

        [JsonProperty("mainImageUrl")] public string MainImageUrl { get; set; }

        [JsonProperty("thumbnailUrl")] public string ThumbnailUrl { get; set; }

        [JsonProperty("description")] public string Description { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("fullUrl")] public string FullUrl { get; set; }
    }
}