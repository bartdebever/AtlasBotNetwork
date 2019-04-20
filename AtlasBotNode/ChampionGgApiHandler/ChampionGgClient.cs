using System;
using ChampionGgApiHandler.Modules;

namespace ChampionGgApiHandler
{
    public class ChampionGgClient
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ChampionGgClient" /> class.
        /// </summary>
        public ChampionGgClient()
        {
            Performance = new PerformanceModule(new Uri("http://api.champion.gg/v2"));
            Champions = new ChampionModule(new Uri("http://api.champion.gg/v2"));
        }

        public IPerformanceModule Performance { get; }

        public IChampionModule Champions { get; }
    }
}