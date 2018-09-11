using SmashggHandler.Models;
using System.Threading.Tasks;

namespace SmashggHandler.Modules.Interfaces
{
    public interface ITournamentEndpoint
    {
        Task<TournamentRoot> GetTournamentByName(string name);

        Task<TournamentScheduleRoot> GetUpcomingTournaments();
    }
}