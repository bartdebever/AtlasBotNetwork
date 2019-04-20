using ChampionGgApiHandler.Models.Champion;
using ChampionGgApiHandler.Models.Performance;
using LoLHandler.Dtos;

namespace AtlasBotNode.EmbedGenerators.ModuleGenerators.Interfaces
{
    /// <summary>
    ///     An interface for generating embeds that belong in the League of Legends genre.
    /// </summary>
    public interface ILeagueEmbedGenerator : IEmbedGenerator
    {
        /// <summary>
        ///     Generates the data about the performance of a champion from champion.gg.
        /// </summary>
        /// <param name="performance">The performance statistics of that champion.</param>
        /// <returns>The same object filled with the new data.</returns>
        ILeagueEmbedGenerator CreatePerformanceEmbed(Performance performance);

        /// <summary>
        ///     Generates the data that is intended to be used to display an embed about a champion's spell.
        /// </summary>
        /// <param name="champion">The champion object that contains the spell information.</param>
        /// <param name="championDto">The data for the champion.</param>
        /// <returns>The same object filled with the new data.</returns>
        ILeagueEmbedGenerator CreateChampionSpellsEmbed(ChampionDto champion, Helpers.ChampionDto championDto);

        /// <summary>
        ///     Generates the data that is intended to be used to show an embed with champion details.
        /// </summary>
        /// <param name="champion">The champion's information</param>
        /// <param name="internalName">The name of the champion.</param>
        /// <returns>The same object filled with the new data.</returns>
        ILeagueEmbedGenerator CreateChampionEmbed(ChampionDto champion, string internalName);

        /// <summary>
        ///     Generates the data that is intended to be used to show the information about the build for that champion.
        ///     This contains all the data like win/loss rate, play rate, etc.
        /// </summary>
        /// <param name="championData">The data for that champion.</param>
        /// <returns>The same object filled with the new data.</returns>
        ILeagueEmbedGenerator CreateChampionDataEmbed(ChampionData championData);

        /// <summary>
        ///     Generates the data that is intended to be used for showing builds for a champion.
        ///     This data is along side skill leveling order and rune builds.
        /// </summary>
        /// <param name="championData">The data for the champion.</param>
        /// <param name="championDto">The general information about the champion.</param>
        /// <returns></returns>
        ILeagueEmbedGenerator CreateChampionBuildEmbed(ChampionData championData, Helpers.ChampionDto championDto);
    }
}