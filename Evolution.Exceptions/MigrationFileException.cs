using System;

namespace Evolution.Exceptions
{
    public class MigrationFileException : Exception
    {
        public MigrationFileException(string message) : base(message) { }
    }
}
