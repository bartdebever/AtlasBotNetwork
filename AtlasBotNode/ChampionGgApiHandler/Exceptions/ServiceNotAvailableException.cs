using System;

namespace ChampionGgApiHandler.Exceptions
{
    public class ServiceNotAvailableException : Exception
    {
        public ServiceNotAvailableException()
        {
        }

        public ServiceNotAvailableException(string message) : base(message)
        {
        }
    }
}