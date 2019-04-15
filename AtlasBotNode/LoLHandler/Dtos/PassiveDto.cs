using Newtonsoft.Json;

namespace LoLHandler.Dtos
{
    public class PassiveDto
    {
        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("description")] public string Description { get; set; }
    }
}