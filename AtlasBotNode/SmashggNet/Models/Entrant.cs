using Newtonsoft.Json;

namespace SmashggNet.Models
{
    /// <summary>
    ///     A class to represent the user who has entered a tournament.
    /// </summary>
    public class Entrant
    {
        /// <summary>
        ///     Gets or sets the name of the entrant.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}