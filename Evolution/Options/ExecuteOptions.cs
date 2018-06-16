using CommandLine;
using Evolution.Model;

namespace Evolution.Options
{
    [Verb("exec", HelpText = "Execute all Evolutions up to the specified Evolution or the last Evolution if no Evolution is specified.")]
    public class ExecuteOptions : DatabaseAuthenticationOptions, IEvolutionTargetable
    {
        [Option("target", HelpText = "Target Evolution", Required = false)]
        public string TargetEvolution { get; set; }
    }
}
