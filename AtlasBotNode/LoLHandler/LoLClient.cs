using System;
using LoLHandler.Modules;

namespace LoLHandler
{
    public class LoLClient
    {
        public ChampionModule Champions { get; }

        public LoLClient()
        {
            Champions = new ChampionModule();
        }
    }
}
