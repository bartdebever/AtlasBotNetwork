using System.Collections.Generic;
using System.Linq;

namespace AtlasBotNode.Helpers
{
    public static class StringCleanerHelper
    {
        public static string NumberToRanking(int number)
        {
            if (number <= 0) return number.ToString();

            if (number % 100 == 11 || number % 100 == 12 || number % 100 == 13)
                return number + "th";

            switch (number % 10)
            {
                case 1:
                    return number + "st";

                case 2:
                    return number + "nd";

                case 3:
                    return number + "rd";

                default:
                    return number + "th";
            }
        }

        public static string SpeedrunTimeConverter(string time)
        {
            string[] characters = { "PT", "H", "M", "S" };
            time = characters.Aggregate(time, (current, character) => current.Replace(character, ":"));
            var chars = new List<string>(time.Split(":"));
            for (var i = 0; i < chars.Count; i++)
            {
                if (chars[i].Length == 1 && i != 0)
                {
                    chars[i] = "0" + chars[i];
                }
            }
            chars.RemoveAt(chars.Count - 1);
            chars.RemoveAt(0);
            return chars.Aggregate((a, b) => a + ":" + b);
        }

        public static string ChampionGgRoleName(string roleName)
        {
            switch (roleName)
            {
                default:
                    return null;

                case "TOP":
                    return "Top";

                case "MIDDLE":
                    return "Mid";

                case "DUO_SUPPORT":
                    return "Support";

                case "DUO_CARRY":
                    return "ADC";

                case "JUNGLE":
                    return "Jungle";
            }
        }

        public static string ToPercentage(this double percentage)
        {
            return $"{percentage:#0.##%}";
        }

        public static string ToChampionGgScore(this double score)
        {
            return $"{score:##.###}";
        }

        public static string ToEmoji(this int id)
        {
            var emote = EmojiHelper.GetEmoji($"{id}");
            if (emote == null)
                return id.ToString();
            return emote.ToString();
        }
    }
}