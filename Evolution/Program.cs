using CommandLine;
using Evolution.IoC;
using Evolution.Options;
using System;
using System.Collections.Generic;

namespace Evolution
{
    public class Program
    {
        public static int Main(string[] args)
        {
            return Parser.Default.ParseArguments<AddOptions, CheckPointOptions, ExecuteOptions>(args).
                MapResult(
                    (AddOptions opts) => Run(opts),
                    (CheckPointOptions opts) => Run(opts),
                    (ExecuteOptions opts) => Run(opts),
                    errors => 1
                );
        }

        private static int Run(AddOptions options)
        {
            try
            {
                var fileRepo = DependencyRegistry.GetFileRepo();
                fileRepo.CreateEvolutionFile(options.TargetEvolution, options.SourceFileName);

                return 0;
            }
            catch(Exception ex)
            {
                Console.WriteLine(GetExceptions(ex));
                return 1;
            }
        }

        private static int Run(CheckPointOptions options)
        {
            throw new NotImplementedException();
        }

        private static int Run(ExecuteOptions options)
        {
            throw new NotImplementedException();
        }

        private static string GetExceptions(Exception ex)
        {
            if (ex == null)
            {
                return string.Empty;
            }
            
            return string.Format("{0} \n {1} \n\n, {2}", ex.Message, ex.StackTrace, GetExceptions(ex.InnerException));
        }
    }
}
