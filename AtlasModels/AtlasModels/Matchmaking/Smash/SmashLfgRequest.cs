using System;
using System.Collections.Generic;
using System.Text;

namespace AtlasModels.Matchmaking.Smash
{
    public class SmashLfgRequest
    {
        public string Username { get; set; }

        public SmashGame Game { get; set; }

        public SmashQueue Queue { get; set; }

        public string Region { get; set; }

        public override string ToString()
        {
            return $"{Username} is looking for {Queue.ToString()} in {Region}";
        }

        public SmashLfgRequest()
        {
        }

        public SmashLfgRequest(string name, SmashGame game, SmashQueue queue, string region)
        {
            Username = name;
            Game = game;
            Queue = queue;
            Region = region;
        }
        public static string GetStringByGame(SmashGame game)
        {
            switch (game)
            {
                case SmashGame.SmashWiiU:
                    return "Super Smash Bros For Wii U";

                case SmashGame.Brawl:
                    return "Super Smash Bros Brawl";

                case SmashGame.Melee:
                    return "Super Smash Bros Melee";

                case SmashGame.Smash64:
                    return "Super Smash Bros (N64)";

                case SmashGame.Smash3DS:
                    return "Super Smash Bros For 3DS";
            }

            return null;
        }
    }
}
