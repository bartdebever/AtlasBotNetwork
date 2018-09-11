using Discord;
using Discord.Commands;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace AtlasBotNode.Modules
{
    [Group("Quiz")]
    public class QuizModule : ModuleBase
    {
        [Command("Question")]
        public async Task CreateQuestion(decimal time, string question, [Optional, Remainder] string answers)
        {
            var answerList = answers?.Split(", ");
            await ReplyAsync("Question has been asked and is available for " + time + " minutes.");
            var embedBuilder = new EmbedBuilder();
            embedBuilder.WithTitle($"Question by {Context.User.Username}");
            embedBuilder.AddField("Question", question);
            var answerText = answerList?.Aggregate("", (current, answer) => current + $"- {answer}\n");
            if (!string.IsNullOrEmpty(answerText))
                embedBuilder.AddField("Answers", answerText);
            embedBuilder.AddField("Time", $"You have {time} minutes so better hurry!");
            await ReplyAsync("", embed: embedBuilder.Build());
        }
    }
}