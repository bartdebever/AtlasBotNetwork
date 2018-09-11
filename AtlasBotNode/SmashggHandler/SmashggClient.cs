using SmashggHandler.Modules;
using SmashggHandler.Modules.Interfaces;

namespace SmashggHandler
{
    public class SmashggClient
    {
        public ITournamentEndpoint TournamentEndpoint { get; }

        public SmashggClient()
        {
            TournamentEndpoint = new TournamentEndpoint();
        }
    }
}