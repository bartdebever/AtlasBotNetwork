using System.Threading.Tasks;
using SmashggHandler.Models;

namespace SmashggHandler.Modules.Interfaces
{
    public interface ITournamentEndpoint
    {
        Task<TournamentRoot> GetTournamentByNameAsync(string name);

        Task<TournamentScheduleRoot> GetUpcomingTournamentsAsync();
    }
}