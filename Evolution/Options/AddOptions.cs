using CommandLine;
using Evolution.Model;

namespace Evolution.Options
{
    [Verb("add")]
    public class AddOptions : DatabaseAuthenticationOptions, IEvolutionCreatable
    {
        [Option("source", HelpText = "Source file to create Evolution")]
        public string SourceFile { get; set; }

        //TODO Add HelpText for AddOptions
        [Option("target", HelpText = "Name of the evolution to create.")]
        public string TargetEvolution { get; set; }
    }
}
