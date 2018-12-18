using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using LoLHandler.Dtos;
using Newtonsoft.Json;

namespace LoLHandler.Modules
{
    public class ChampionModule
    {
        //TODO make this configurable
        private string _baseFolder = $@"D:\LoLData8\8.19.1\data\en_GB\champion";

        public async Task<ChampionDto> GetChampionByNameAsync(string name)
        {
            var file = $"{_baseFolder}/{name}.json";
            if (!File.Exists(file))
                return null;
            string lines;
            var fileStream = new FileStream(file, FileMode.Open);
            using (var reader = new StreamReader(fileStream))
            {
                lines = await reader.ReadToEndAsync();
            }

            var root = JsonConvert.DeserializeObject<ChampionRoot>(lines);
            return root?.Champions[name];
        }
    }
}
