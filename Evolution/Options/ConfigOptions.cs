using CommandLine;

namespace Evolution.Options
{
    //TODO Implement Configuration Options
    [Verb("config", HelpText = "Evolution Configuration Options")]
    public class ConfigOptions
    {
        [Option("db")]
        public string DatabaseType { get; set; }
    }
}
