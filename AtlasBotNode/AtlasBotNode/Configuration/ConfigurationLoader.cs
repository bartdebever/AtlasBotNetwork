using System;
using System.IO;
using AtlasBotNode.Configuration.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace AtlasBotNode.Configuration
{
    public class ConfigurationLoader : IConfigurationLoader<string>
    {

        public BaseConfiguration CreateConfiguration(string input)
        {
            if (!File.Exists(input))
            {
                // TODO proper error handling
                throw new ArgumentException(nameof(input));
            }
            
            var jsonData = File.ReadAllText(input);

            return JsonConvert.DeserializeObject<BaseConfiguration>(jsonData);
        }
    }
}