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

        /// <summary>
        /// Gets a <see cref="ChampionDto"/> object based on the given <paramref name="name"/>.
        /// </summary>
        /// <param name="name">
        /// The name of the champion wanting to be searched.
        /// <para>Note that this works off the internal League of Legends names.</para>
        /// <para>That means that Wukong will be MonkeyKing and others will be renamed as well.</para>
        /// </param>
        /// <returns>
        /// The <see cref="ChampionDto"/> object created from the JSON file.
        /// <para>Returns null when no champion with that name has been found.</para>
        /// </returns>
        public async Task<ChampionDto> GetChampionByNameAsync(string name)
        {
            var file = $"{_baseFolder}/{name}.json";
            if (!File.Exists(file))
            {
                return null;
            }

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
