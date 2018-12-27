using AtlasBotNode.EmbedGenerators;
using AtlasBotNode.Loggers;
using Discord;
using Discord.Commands;
using System;
using System.Threading.Tasks;

namespace AtlasBotNode.Modules
{
    [Group("test")]
    public class TestModule : ModuleBase
    {
        private readonly IDefaultEmbedGenerator _defaultEmbedGenerator;

        public TestModule(IDefaultEmbedGenerator defaultEmbedGenerator)
        {
            _defaultEmbedGenerator = defaultEmbedGenerator;
        }

        [Command("Test")]
        public async Task Test()
        {
            var embed = _defaultEmbedGenerator.GenerateNotFoundEmbed("TestModule", "TestCommand", "Title", "Command was not found!").Build();
            await DefaultLogger.Logger(new LogMessage(LogSeverity.Error, "TestModule", "TestModule has crashed!",
                new DllNotFoundException()));
            await ReplyAsync("", embed: embed);
        }
    }
}