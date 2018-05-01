using CommandLine;
using Evolution.Model;

namespace Evolution.Options
{
    [Verb("check")]
    public class CheckPointOptions : DatabaseAuthenticationOptions, IEvolutionTargetable
    {
        [Option("target", HelpText = "Target Evolution", Required = false)]
        public string TargetEvolution { get; set; }
    }
}
