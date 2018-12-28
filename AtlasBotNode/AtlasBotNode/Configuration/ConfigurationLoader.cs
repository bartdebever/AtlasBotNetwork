namespace AtlasBotNode.Configuration
{
    using System;
    using System.IO;
    using Models;
    using Newtonsoft.Json;

    /// <summary>
    /// Creates a configuration based on a JSON file.
    /// </summary>
    public class ConfigurationLoader : IConfigurationLoader<string>
    {
        /// <summary>
        /// Creates a configuration object based on the provided JSON file.
        /// </summary>
        /// <param name="input">The path towards the JSON settings file.</param>
        /// <returns>An instance of the <see cref="BaseConfiguration"/>.</returns>
        /// <inheritdoc />
        public BaseConfiguration CreateConfiguration(string input)
        {
            if (!File.Exists(input))
            {
                throw new FileNotFoundException();
            }

            var jsonData = File.ReadAllText(input);

            return JsonConvert.DeserializeObject<BaseConfiguration>(jsonData);
        }
    }
}