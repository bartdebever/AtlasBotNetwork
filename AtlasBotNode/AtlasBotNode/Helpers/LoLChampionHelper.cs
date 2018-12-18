using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace AtlasBotNode.Helpers
{
    public static class LoLChampionHelper
    {
        private static Dictionary<string, ChampionDto> _championData = new Dictionary<string, ChampionDto>();
        private static bool _initialized;

        public static ChampionDto GetChampionByName(string name)
        {
            ConvertToChampionName(ref name);
            return _championData[name];
        }
        public static int GetIdFromName(string name)
        {
            ConvertToChampionName(ref name);
            return _championData.ContainsKey(name) ? _championData[name].Id : 0;
        }
        /// <summary>
        /// Gets the name and id of a champion based on the provided name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Tuple<int, string> GetNameAndIdForName(string name)
        {
            ConvertToChampionName(ref name);
            return _championData.ContainsKey(name) ? new Tuple<int, string>(_championData[name].Id, _championData[name].Name) : null;
        }
        public static string GetNameFromId(int id)
        {
            var champion = _championData.FirstOrDefault(x => x.Value.Id == id);
            return champion.Value?.Name;
        }
        public static void ConvertToChampionName(ref string name)
        {
            if(!_initialized)
                InitializeDictionary();
            name = Regex.Replace(name.ToLower(), "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled);

            CheckForSpecialCases(ref name);
        }

        private static void CheckForSpecialCases(ref string name)
        {
            switch (name)
            {
                case "kha":
                    name = "khazix";
                    break;
                case "xin":
                    name = "xinzhao";
                    break;
            }
        }

        private static void InitializeDictionary()
        {
            //TODO Convert this to environment variable
            var file = @"D:\LoLData\6.24.1\data\en_GB\FormatedChampions.json";
            var fileStream = new FileStream(file, FileMode.Open);
            using (var reader = new StreamReader(fileStream))
            {
                _championData = JsonConvert.DeserializeObject<Dictionary<string,ChampionDto>>(reader.ReadToEnd());
            }

            _initialized = true;
        }
    }

    /// <summary>
    /// The data we actually want/need from a champion
    /// </summary>
    public class ChampionDto
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// The name internally used to describe the champion for files.
        /// Can be completely different from the champion like "Wukong" being "MonkeyKing"
        /// </summary>
        [JsonProperty("internalName")]
        public string InternalName { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
    }
}
