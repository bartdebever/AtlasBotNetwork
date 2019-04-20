using Discord;

namespace AtlasBotNode.EmbedGenerators
{
    /// <summary>
    ///     An interface to define how classes look that will generate embeds.
    /// </summary>
    public interface IEmbedGenerator
    {
        /// <summary>
        ///     Builds the embed from the internal builder after the used commands.
        /// </summary>
        /// <returns>The embed which can be send to the user.</returns>
        Embed Build();
    }
}