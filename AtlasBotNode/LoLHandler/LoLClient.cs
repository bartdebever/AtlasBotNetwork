using LoLHandler.Modules;

namespace LoLHandler
{
    public class LoLClient
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ChampionModule" /> class.
        /// </summary>
        public LoLClient()
        {
            Champions = new ChampionModule();
        }

        public ChampionModule Champions { get; }
    }
}