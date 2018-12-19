using AtlasBotNode.Configuration.Models;

namespace AtlasBotNode.Configuration
{
    public interface IConfigurationLoader<T>
    {
        /// <summary>
        /// Creates a configuration based on the given <paramref name="input"/>
        /// </summary>
        /// <param name="input">The input of the type specific to this implementation.</param>
        /// <returns>A fully filled configuration</returns>
        BaseConfiguration CreateConfiguration(T input);
    }
}