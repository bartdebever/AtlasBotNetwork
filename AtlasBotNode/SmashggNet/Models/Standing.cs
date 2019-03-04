namespace SmashggNet.Models
{
    using Newtonsoft.Json;

    public class Standing
    {
        /// <summary>
        /// Gets or sets the ranking that this entrant, specified in <see cref="Entrant"/>, has gotten.
        /// </summary>
        [JsonProperty("standing")]
        public int Ranking { get; set; }

        /// <summary>
        /// Gets or sets the entrant who has gotten the ranking specified in <see cref="Ranking"/>
        /// </summary>
        [JsonProperty("entrant")]
        public Entrant Entrant { get; set; }
    }
}
