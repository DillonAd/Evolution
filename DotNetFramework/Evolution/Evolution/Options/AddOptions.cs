using CommandLine;
using Evolution.Model;

namespace Evolution.Options
{
    [Verb("add")]
    public class AddOptions : DatabaseAuthenticationOptions, IEvolutionCreatable
    {
        [Option("src", HelpText = "Source file to create Evolution")]
        public string SourceFile { get; set; }
        [Option("evo", HelpText = "")]
        public string TargetEvolution { get; set; }
    }
}
