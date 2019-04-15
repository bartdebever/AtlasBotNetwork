using System.Threading.Tasks;
using SmashggNet.Models;

namespace SmashggNet.Modules.Interfaces
{
    public interface ITournamentModule
    {
        /// <summary>
        ///     Gets the current/finished standings of the tournament.
        /// </summary>
        /// <param name="tournament">The slug or full tournament name.</param>
        /// <returns>
        ///     An awaitable task with the result being a tournament filled with events and standings.
        ///     <para>Will return null when the tournament is not found.</para>
        /// </returns>
        Task<Tournament> GetTournamentStandings(string tournament);
    }
}