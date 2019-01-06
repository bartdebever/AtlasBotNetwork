using System;
using LoLHandler.Modules;

namespace LoLHandler
{
    public class LoLClient
    {
        public ChampionModule Champions { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChampionModule"/> class.
        /// </summary>
        public LoLClient()
        {
            Champions = new ChampionModule();
        }
    }
}
