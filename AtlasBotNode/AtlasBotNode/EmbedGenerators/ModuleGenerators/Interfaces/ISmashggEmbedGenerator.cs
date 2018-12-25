using SmashggHandler.Models;

namespace AtlasBotNode.EmbedGenerators.ModuleGenerators.Interfaces
{
    /// <summary>
    /// An interface to generates embeds based on the smash.gg platform.
    /// </summary>
    public interface ISmashggEmbedGenerator : IEmbedGenerator
    {
        /// <summary>
        /// Generates the data intended to be used to generate a tournament embed.
        /// </summary>
        /// <param name="tournament">The data of the tournament.</param>
        /// <returns>The same object extended with the new data.</returns>
        ISmashggEmbedGenerator CreateTournamentEmbed(TournamentRoot tournament);

        /// <summary>
        /// Generates the data intended to be used to generate an embed displaying a schedule of upcoming tournaments.
        /// </summary>
        /// <param name="tournaments">The tournaments wanting to be shown.</param>
        /// <returns>The same object extended with the new data.</returns>
        ISmashggEmbedGenerator CreateTournamentScheduleEmbed(TournamentScheduleRoot tournaments);

        /// <summary>
        /// Generates the data to create a match embed.
        /// </summary>
        /// <returns>The same object exnteded with the new data.</returns>
        ISmashggEmbedGenerator CreateMatchEmbedGenerator();
    }
}