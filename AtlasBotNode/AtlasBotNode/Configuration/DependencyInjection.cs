using AtlasBotNode.EmbedGenerators;
using AtlasBotNode.EmbedGenerators.ModuleGenerators;
using AtlasBotNode.EmbedGenerators.ModuleGenerators.Interfaces;
using AtlasBotNode.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace AtlasBotNode.Configuration
{
    public static class DependencyInjection
    {
        private static ServiceCollection _serviceCollection;

        /// <summary>
        ///     Injects all objects into a <see cref="ServiceCollection" />. Intended to be used to add dependency injection to the
        ///     commands.
        /// </summary>
        /// <returns>A collection of injectable services.</returns>
        public static ServiceCollection GetServiceCollection()
        {
            _serviceCollection = new ServiceCollection();

            InjectEmbedGenerators();
            InjectHelpers();

            return _serviceCollection;
        }

        /// <summary>
        ///     Injects all EmbedGenerators objects into the <see cref="_serviceCollection" /> object.
        /// </summary>
        private static void InjectEmbedGenerators()
        {
            _serviceCollection.AddTransient<IDefaultEmbedGenerator, DefaultEmbedGenerator>();
            _serviceCollection.AddTransient<IHelpEmbedGenerator, HelpEmbedGenerator>();
            _serviceCollection.AddTransient<ISmashggEmbedGenerator, SmashggEmbedGenerator>();
            _serviceCollection.AddTransient<ISpeedrunEmbedGenerator, SpeedrunEmbedGenerator>();
            _serviceCollection.AddTransient<ISmash4EmbedGenerator, Smash4EmbedGenerator>();
            _serviceCollection.AddTransient<ILeagueEmbedGenerator, LeagueEmbedGenerator>();
            _serviceCollection.AddTransient<IYoutubeEmbedGenerator, YoutubeEmbedGenerator>();
        }

        /// <summary>
        ///     Injects all helpers objects into the <see cref="_serviceCollection" /> object.
        /// </summary>
        private static void InjectHelpers()
        {
            _serviceCollection.AddTransient<IInputSanitizer, InputSanitizer>();
        }
    }
}