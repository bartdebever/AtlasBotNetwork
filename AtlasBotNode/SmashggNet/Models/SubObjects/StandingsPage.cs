using System.Collections.Generic;
using Newtonsoft.Json;

namespace SmashggNet.Models.SubObjects
{
    public class StandingsPage
    {
        /// <summary>
        ///     Gets or sets the standings of the event. These can be current or finished.
        /// </summary>
        [JsonProperty("nodes")]
        public List<Standing> Standings { get; set; }
    }
}