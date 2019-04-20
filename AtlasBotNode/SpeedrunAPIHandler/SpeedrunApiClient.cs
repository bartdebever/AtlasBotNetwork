using SpeedrunAPIHandler.Modules;

namespace SpeedrunAPIHandler
{
    public class SpeedrunApiClient
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SpeedrunApiClient" /> class.
        /// </summary>
        public SpeedrunApiClient()
        {
            LeaderboardModule = new LeaderboardModule();
        }

        public static string ApiKey { get; set; }

        public ILeaderboardModule LeaderboardModule { get; }
    }
}