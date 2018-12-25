using KhApiHandler.Models;

namespace AtlasBotNode.EmbedGenerators.ModuleGenerators.Interfaces
{
    /// <summary>
    /// An interface to generate embeds in the Smash 4 scope.
    /// </summary>
    public interface ISmash4EmbedGenerator : IEmbedGenerator
    {
        /// <summary>
        /// Generates the data intended to be used to display an embed of a Smash 4 character.
        /// </summary>
        /// <param name="character">The data of the character wanting to be displayed.</param>
        /// <returns>The same object extended with the new data.</returns>
        ISmash4EmbedGenerator CreateCharacterEmbed(Character character);
    }
}