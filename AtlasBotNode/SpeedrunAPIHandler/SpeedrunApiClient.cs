using SpeedrunAPIHandler.Modules;

namespace SpeedrunAPIHandler
{
    public class SpeedrunApiClient
    {
        public static string ApiKey { get; set; }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="SpeedrunApiClient"/> class.
        /// </summary>
        public SpeedrunApiClient()
        {
            LeaderboardModule = new LeaderboardModule();
        }

        public ILeaderboardModule LeaderboardModule { get; }
    }
}