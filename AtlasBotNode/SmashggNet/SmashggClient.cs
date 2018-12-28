namespace SmashggNet
{
    using Modules;

    public class SmashggClient
    {
        /// <summary>
        /// Gets or sets the token used to authenticate towards the API.
        /// This should be set once before executing any request.
        /// </summary>
        public static string ApiToken { get; set; }

        /// <summary>
        /// Gets the tournament module.
        /// </summary>
        public static TournamentModule Tournament => new TournamentModule();
    }
}
