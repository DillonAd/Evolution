using CommandLine;

namespace Evolution.Options
{
    [Verb("add")]
    public class AddOptions
    {
        [Option("src", HelpText = "Source file to create Evolution")]
        public string SourceFileName { get; set; }
        [Option("evo", HelpText = "")]
        public string EvolutionFileName { get; set; }
    }
}
