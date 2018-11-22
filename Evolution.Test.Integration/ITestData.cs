using System.Collections.Generic;

namespace Evolution.Test.Integration
{
    public interface ITestData
    {
        string FilePath { get; }
        string User { get; }
        string Password { get; }
        string Server { get; }
        string Instance { get; }
        string Port { get; }
        string Type { get; }
        bool UseConfig { get; }
    }
}