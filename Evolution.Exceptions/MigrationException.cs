using System;

namespace Evolution.Exceptions
{
    public class MigrationException : Exception
    {
        public MigrationException(string message, Exception innerException) : base(message, innerException) { }
    }
}
