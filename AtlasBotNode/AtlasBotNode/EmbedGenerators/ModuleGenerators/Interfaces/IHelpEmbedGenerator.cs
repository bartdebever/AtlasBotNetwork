using System.Collections.Generic;
using Discord.Commands;

namespace AtlasBotNode.EmbedGenerators.ModuleGenerators.Interfaces
{
    /// <summary>
    /// An interface to define how help embeds will be generated.
    /// </summary>
    public interface IHelpEmbedGenerator : IEmbedGenerator
    {
        /// <summary>
        /// Generates a list of commands with the help text with it.
        /// </summary>
        /// <param name="modules">The list of information about the modules in the current node.</param>
        /// <returns>The same object with added information in the embed.</returns>
        IHelpEmbedGenerator GenerateHelpListEmbed(IEnumerable<ModuleInfo> modules);
    }
}