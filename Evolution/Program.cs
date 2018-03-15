using CommandLine;
using Evolution.Options;
using System;

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
            throw new NotImplementedException();
        }

        private static int Run(CheckPointOptions options)
        {
            throw new NotImplementedException();
        }

        private static int Run(ExecuteOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
