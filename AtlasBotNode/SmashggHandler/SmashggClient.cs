using SmashggHandler.Modules;
using SmashggHandler.Modules.Interfaces;

namespace SmashggHandler
{
    public class SmashggClient
    {
        public ITournamentEndpoint TournamentEndpoint { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SmashggClient"/> class.
        /// </summary>
        public SmashggClient()
        {
            TournamentEndpoint = new TournamentEndpoint();
        }
    }
}