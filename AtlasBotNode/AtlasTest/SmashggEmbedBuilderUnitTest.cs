using AtlasBotNode.EmbedGenerators.ModuleGenerators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmashggHandler.Models;
using Color = Discord.Color;

namespace AtlasTest
{
    [TestClass]
    public class SmashggEmbedBuilderUnitTest
    {
        private SmashggEmbedGenerator _smashggEmbedGenerator;

        [TestInitialize]
        public void TestSetup()
        {
            _smashggEmbedGenerator = new SmashggEmbedGenerator();
        }

        [TestMethod]
        public void CreateTournamentEmbed()
        {
            var embed = _smashggEmbedGenerator.CreateTournamentEmbed(new TournamentRoot { Entities = new TournamentEntities { Tournament = new Tournament { Name = "Test" } } }).Build();
            Assert.IsTrue(embed.Color.HasValue);
            Assert.AreEqual(Color.Blue, embed.Color.Value);
        }
    }
}