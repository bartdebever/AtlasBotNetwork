using ChampionGgApiHandler.Modules;
using System;

namespace ChampionGgApiHandler
{
    public class ChampionGgClient
    {
        public IPerformanceModule Performance { get; }
        
        public IChampionModule Champions { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChampionGgClient"/> class.
        /// </summary>
        public ChampionGgClient()
        {
            Performance = new PerformanceModule(new Uri("http://api.champion.gg/v2"));
            Champions = new ChampionModule(new Uri("http://api.champion.gg/v2"));
        }
    }
}