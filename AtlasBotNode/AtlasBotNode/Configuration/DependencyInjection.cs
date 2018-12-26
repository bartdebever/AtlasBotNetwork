namespace AtlasBotNode.Configuration
{
    using EmbedGenerators;
    using EmbedGenerators.ModuleGenerators;
    using EmbedGenerators.ModuleGenerators.Interfaces;
    using Helpers;
    using Microsoft.Extensions.DependencyInjection;

    public static class DependencyInjection
    {
        private static ServiceCollection _serviceCollection;

        /// <summary>
        /// Injects all objects into a <see cref="ServiceCollection"/>. Intended to be used to add dependency injection to the commands.
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
        /// Injects all EmbedGenerators objects into the <see cref="_serviceCollection"/> object.
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
        /// Injects all helpers objects into the <see cref="_serviceCollection"/> object.
        /// </summary>
        private static void InjectHelpers()
        {
            _serviceCollection.AddTransient<IInputSanitizer, InputSanitizer>();
        }
    }
}