using System;
using System.Runtime.Serialization;

namespace Evolution.Exceptions
{
    [Serializable]
    public class EvolutionFileException : Exception
    {
        public EvolutionFileException() { }

        public EvolutionFileException(string message) : base(message) { }

        public EvolutionFileException(string message, Exception innerException) : base(message, innerException) { }

        protected EvolutionFileException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
