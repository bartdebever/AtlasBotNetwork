using ChampionGgApiHandler.Modules;
using System;

namespace ChampionGgApiHandler
{
    public class ChampionGgClient
    {
        public IPerformanceModule Performance { get; }
        public IChampionModule Champions { get; }

        public ChampionGgClient()
        {
            Performance = new PerformanceModule(new Uri("http://api.champion.gg/v2"));
            Champions = new ChampionModule(new Uri("http://api.champion.gg/v2"));
        }
    }
}