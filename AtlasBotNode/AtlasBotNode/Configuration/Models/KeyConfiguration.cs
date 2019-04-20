namespace AtlasBotNode.Configuration.Models
{
    /// <summary>
    ///     A class used to store the keys configured in AtlasBot.
    ///     Intended to be used for specific APIs
    /// </summary>
    public class KeyConfiguration
    {
        /// <summary>
        ///     Gets or sets the key used for Discord API.
        /// </summary>
        public string Discord { get; set; }

        /// <summary>
        ///     Gets or sets the key used for the YouTube API.
        /// </summary>
        public string YouTube { get; set; }

        /// <summary>
        ///     Gets or sets the key used for the Champion.gg API.
        /// </summary>
        public string Championgg { get; set; }

        /// <summary>
        ///     Gets or sets the key used for the Speedrun.com API.
        /// </summary>
        public string Speedrun { get; set; }

        /// <summary>
        ///     Gets or sets the token used for the smash.gg API.
        /// </summary>
        public string Smashgg { get; set; }
    }
}