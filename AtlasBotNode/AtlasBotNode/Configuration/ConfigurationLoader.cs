using AtlasBotNode.Configuration.Models;
using Microsoft.Extensions.Configuration;

namespace AtlasBotNode.Configuration
{
    public class ConfigurationLoader : IConfigurationLoader<IConfiguration>
    {
        public BaseConfiguration CreateConfiguration<IConfiguration>(IConfiguration input)
        {
            throw new System.NotImplementedException();
        }
    }
}