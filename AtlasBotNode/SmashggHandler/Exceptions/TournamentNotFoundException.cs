using System;

namespace SmashggHandler.Exceptions
{
    [Serializable]
    public class TournamentNotFoundException : Exception
    {
        public TournamentNotFoundException()
        {
        }

        public TournamentNotFoundException(string message) : base(message)
        {
        }
    }
}