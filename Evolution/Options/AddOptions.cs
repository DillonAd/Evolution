using CommandLine;
using Evolution.Model;

namespace Evolution.Options
{
    [Verb("add", HelpText = "Creates an Evolution file from a specified source file.")]
    public class AddOptions : DatabaseAuthenticationOptions, IEvolutionCreatable
    {
        [Option("source", HelpText = "Source file to create Evolution")]
        public string SourceFile { get; set; }

        [Option("target", HelpText = "Name of the evolution to create.")]
        public string TargetEvolution { get; set; }
    }
}
