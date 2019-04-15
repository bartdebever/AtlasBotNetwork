using YoutubeApiHandler.Results;

namespace AtlasBotNode.EmbedGenerators.ModuleGenerators.Interfaces
{
    /// <summary>
    ///     An interfaces for generating YouTube based embeds.
    /// </summary>
    public interface IYoutubeEmbedGenerator : IEmbedGenerator
    {
        /// <summary>
        ///     Generates the data that is intended to be used to create an embed to display channel information.
        /// </summary>
        /// <param name="channel">The information of the channel wanting to be shown.</param>
        /// <returns>The same object with the new data.</returns>
        IYoutubeEmbedGenerator CreateChannelEmbed(YoutubeChannelResult channel);
    }
}