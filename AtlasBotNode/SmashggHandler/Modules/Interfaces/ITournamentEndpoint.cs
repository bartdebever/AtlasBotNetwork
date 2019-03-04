using SmashggHandler.Models;
using System.Threading.Tasks;

namespace SmashggHandler.Modules.Interfaces
{
    public interface ITournamentEndpoint
    {
        Task<TournamentRoot> GetTournamentByNameAsync(string name);

        Task<TournamentScheduleRoot> GetUpcomingTournamentsAsync();
    }
}