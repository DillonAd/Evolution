using System;

namespace Evolution.Exceptions
{
    public class EvolutionException : Exception
    {
        public EvolutionException(string message, Exception innerException) : base(message, innerException) { }
    }
}
