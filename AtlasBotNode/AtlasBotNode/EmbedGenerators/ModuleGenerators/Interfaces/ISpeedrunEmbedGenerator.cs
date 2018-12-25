using SpeedrunAPIHandler.Models;

namespace AtlasBotNode.EmbedGenerators.ModuleGenerators.Interfaces
{
    /// <summary>
    /// An interface to generate embeds based on the speedrun leaderboards.
    /// </summary>
    public interface ISpeedrunEmbedGenerator : IEmbedGenerator
    {
        /// <summary>
        /// Generates data that is intended to be used to show a leaderboard of the specified game.
        /// </summary>
        /// <param name="leaderboard">The leaderboard wanting to be shown.</param>
        /// <returns>The same object with the new data inside it.</returns>
        ISpeedrunEmbedGenerator CreateLeaderboardEmbed(Leaderboard leaderboard);

        /// <summary>
        /// Generates data that is intended to be used to show the world record of a specific game.
        /// </summary>
        /// <param name="leaderboard">The leaderboards that contain the world record data.</param>
        /// <returns>The same object with the new data inside it.</returns>
        ISpeedrunEmbedGenerator CreateWorldRecordEmbed(Leaderboard leaderboard);
    }
}