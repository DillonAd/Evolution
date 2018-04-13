using CommandLine;
using Evolution.Model;

namespace Evolution.Options
{
    [Verb("cp")]
    public class CheckPointOptions : DatabaseAuthenticationOptions, IEvolutionTargetable
    {
        [Option("user", HelpText = "Database UserName")]
        public string UserName { get; set; }
        [Option("pwd", HelpText = "Database Password")]
        public string Password { get; set; }
        [Option("svr", HelpText = "Database Server Name")]
        public string Server { get; set; }
        [Option("i", HelpText = "Database Instance Name")]
        public string Instance { get; set; }
        //TODO DatabaseType on CheckPointOptions
        public string DatabaseType { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        [Option("evo", HelpText = "Target Evolution", Required = false)]
        public string TargetEvolution { get; set; }
    }
}
