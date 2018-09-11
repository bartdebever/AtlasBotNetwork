using Newtonsoft.Json;

namespace ChampionGgApiHandler.Models.Champion
{
    public class DamageData
    {
        [JsonProperty("total")]
        public double Total { get; set; }

        [JsonProperty("totalTrue")]
        public double TotalTrue { get; set; }

        [JsonProperty("totalMagical")]
        public double TotalMagic { get; set; }

        [JsonProperty("totalPhysical")]
        public double TotalPhysical { get; set; }

        [JsonProperty("percentTrue")]
        public double PercentageTrue { get; set; }

        [JsonProperty("percentMagical")]
        public double PercentageMagic { get; set; }

        [JsonProperty("percentPhysical")]
        public double PercentagePhysical { get; set; }
    }
}