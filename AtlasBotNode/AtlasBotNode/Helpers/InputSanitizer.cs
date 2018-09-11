namespace AtlasBotNode.Helpers
{
    public interface IInputSanitizer
    {
        string SmashggTournamentReplacement(string text);
    }

    public class InputSanitizer : IInputSanitizer
    {
        public string SmashggTournamentReplacement(string text)
        {
            return text.Replace(' ', '-');
        }
    }
}