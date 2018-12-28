namespace SmashggNet
{
    using Modules;

    public class SmashggNewClient
    {
        public static string ApiToken { get; set; }

        public TournamentModule Tournament { get; set; } = new TournamentModule();
    }
}
