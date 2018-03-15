using CommandLine;

namespace Evolution.Options
{
    [Verb("exec")]
    public class ExecuteOptions : IDatabaseAuthenticationOptions, ITargetEvolutionOptions
    {
        [Option("user", HelpText = "Database UserName")]
        public string UserName { get; set; }
        [Option("pwd", HelpText = "Database Password")]
        public string Password { get; set; }
        [Option("svr", HelpText = "Database Server Name")]
        public string Server { get; set; }
        [Option("i", HelpText = "Database Instance Name")]
        public string Instance { get; set; }
        [Option("evo", HelpText = "Target Evolution", Required = false)]
        public string TargetEvolution { get; set; }
    }
}
