using AtlasBotNode.Configuration.Models;

namespace AtlasBotNode.Configuration
{
    public interface IConfigurationLoader<T>
    {
        BaseConfiguration CreateConfiguration(T input);
    }
}