using System;

namespace Evolution.Exceptions
{
    public class InvalidConfigurationException : Exception
    {
        public InvalidConfigurationException() : base() { }

        public InvalidConfigurationException(string message) : base(message) { }
    }
}