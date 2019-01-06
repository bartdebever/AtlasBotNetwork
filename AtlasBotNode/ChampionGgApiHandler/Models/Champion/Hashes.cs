using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ChampionGgApiHandler.Models.Champion
{
    /// <summary>
    /// This class contains all data about skills, masteries, items and summoners
    /// </summary>
    public class Hashes
    {
        [JsonProperty("finalitemshashfixed")]
        public HashCategory Items { get; set; }
        
        [JsonProperty("masterieshash")]
        public HashCategory Masteries { get; set; }
        
        [JsonProperty("skillorderhash")]
        public HashCategory Skills { get; set; }
        
        [JsonProperty("summonershash")]
        public HashCategory Summoners { get; set; }
        
        [JsonProperty("runehash")]
        public HashCategory Runes { get; set; }
        
        [JsonProperty("firstitemshash")]
        public HashCategory FirstItems { get; set; }
    }

    public class HashCategory
    {
        [JsonProperty("highestCount")]
        public InnerHash MostUsed { get; set; }

        [JsonProperty("highestWinrate")]
        public InnerHash HighestWinrate { get; set; }
    }

    public class InnerHash
    {
        [JsonProperty("count")]
        public int Count { get; set; }
        
        [JsonProperty("wins")]
        public int Wins { get; set; }
        
        [JsonProperty("winrate")]
        public double Winrate { get; set; }
        
        [JsonProperty("hash")]
        public string Hash { get; set; }
    }
}
