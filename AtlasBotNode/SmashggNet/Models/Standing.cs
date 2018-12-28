namespace SmashggNet.Models
{
    using Newtonsoft.Json;

    public class Standing
    {
        [JsonProperty("standing")]
        public int Ranking { get; set; }

        [JsonProperty("entrant")]
        public Entrant Entrant { get; set; }
    }
}
