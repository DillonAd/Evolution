using CommandLine;
using Evolution.Model;

namespace Evolution.Options
{
    [Verb("exec")]
    public class ExecuteOptions : DatabaseAuthenticationOptions, IEvolutionTargetable
    {
        //TODO haz needs exec docs
        [Option("target", HelpText = "Target Evolution", Required = false)]
        public string TargetEvolution { get; set; }
    }
}
