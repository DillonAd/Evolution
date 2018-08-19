using System;

namespace Evolution.Configuration
{
    public class InvalidConfigurationException : Exception
    {
        public InvalidConfigurationException() : base() { }

        public InvalidConfigurationException(string message) : base(message) { }
    }
}