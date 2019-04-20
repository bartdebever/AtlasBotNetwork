using System;
using System.Collections.Generic;
using System.Text;
using AtlasBotNode.EmbedGenerators;
using AtlasBotNode.EmbedGenerators.ModuleGenerators;
using AtlasBotNode.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace AtlasBotNode.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddEmbedGenerators(this IServiceCollection services)
        {
            services.AddTransient<IDefaultEmbedGenerator, DefaultEmbedGenerator>();
            services.AddTransient<IHelpEmbedGenerator, HelpEmbedGenerator>();
            services.AddTransient<IInputSanitizer, InputSanitizer>();
            services.AddTransient<ISmashggEmbedGenerator, SmashggEmbedGenerator>();
            services.AddTransient<ISpeedrunEmbedGenerator, SpeedrunEmbedGenerator>();
            services.AddTransient<ISmash4EmbedGenerator, Smash4EmbedGenerator>();
            services.AddTransient<ILeagueEmbedGenerator, LeagueEmbedGenerator>();
            services.AddTransient<IYoutubeEmbedGenerator, YoutubeEmbedGenerator>();
            return services;
        }
    }
}
