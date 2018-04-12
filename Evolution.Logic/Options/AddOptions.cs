using CommandLine;
using Evolution.Model;

namespace Evolution.Logic.Options
{
    [Verb("add")]
    public class AddOptions : ITargetEvolutionOptions
    {
        [Option("src", HelpText = "Source file to create Evolution")]
        public string SourceFileName { get; set; }
        [Option("evo", HelpText = "")]
        public string TargetEvolution { get; set; }
    }
}
