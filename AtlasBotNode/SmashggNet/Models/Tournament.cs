using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace SmashggNet.Models
{
    public class Tournament
    {
        /// <summary>
        /// Gets or sets the name of the tournament.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a collection of events that this tournament has.
        /// </summary>
        [JsonProperty("events")]
        public List<Event> Event { get; set; }
    }
}
