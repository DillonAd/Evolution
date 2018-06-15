using System;
using System.Runtime.Serialization;

namespace Evolution.Exceptions
{
    [Serializable]
    public class EvolutionException : Exception
    {
        public EvolutionException() { }

        public EvolutionException(string message) : base(message) { }

        public EvolutionException(string message, Exception innerException) : base(message, innerException) { }

        protected EvolutionException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
