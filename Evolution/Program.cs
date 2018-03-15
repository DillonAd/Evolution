using CommandLine;
using Evolution.Options;

namespace Evolution
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<AddOptions, CheckPointOptions, ExecuteOptions>(args).
                MapResult(
                    (AddOptions opts) => Run(opts),
                    (CheckPointOptions opts) => Run(opts),
                    (ExecuteOptions opts) => Run(opts),
                    errs => 1
                );
        }

        private static int Run(AddOptions options)
        {
            
        }

        private static int Run(CheckPointOptions options)
        {

        }

        private static int Run(ExecuteOptions options)
        {

        }
    }
}
