using CommandLine;
using Evolution.Model;
using Evolution.Data.Oracle;
using Evolution.Options;
using System;

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
                var evolution = new Model.Evolution(new Date(), options.TargetEvolution);

                fileRepo.CreateEvolutionFile(evolution.FileName, options.SourceFileName);

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
            try
            {
                var evolutionRepo = DependencyRegistry.GetEvolutionRepo<OracleEvolutionContext, OracleConnectionBuilder>(options);
                var fileRepo = DependencyRegistry.GetFileRepo();

                var executedEvolutions = evolutionRepo.GetExecutedEvolutionFileNames();
                var unexecutedEvolutionFiles = fileRepo.GetUnexecutedEvolutionFiles(executedEvolutions);
                //TODO get most recent evolution name by filename if no evolution was provided
                string fileContents;

                foreach(var evolutionFile in unexecutedEvolutionFiles)
                {
                    fileContents = fileRepo.GetEvolutionFileContent(evolutionFile);
                    evolutionRepo.ExecuteEvolution(fileContents);
                    //TODO
                    //evolutionRepo.AddEvolution();
                }

                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(GetExceptions(ex));
                return 1;
            }
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
