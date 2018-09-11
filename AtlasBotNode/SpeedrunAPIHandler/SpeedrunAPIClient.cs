using SpeedrunAPIHandler.Modules;

namespace SpeedrunAPIHandler
{
    public class SpeedrunAPIClient
    {
        public static string ApiKey { get; set; }
        public SpeedrunAPIClient()
        {
            LeaderboardModule = new LeaderboardModule();
        }

        public ILeaderboardModule LeaderboardModule { get; }
    }
}