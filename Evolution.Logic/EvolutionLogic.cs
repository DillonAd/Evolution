using Evolution.Model;
using Evolution.Repo;
using System;

namespace Evolution.Logic
{
    public class EvolutionLogic : IEvolutionLogic
    {
        private readonly IEvolutionRepo _EvolutionRepo;
        private readonly IFileRepo _FileRepo;

        public EvolutionLogic(IEvolutionRepo evolutionRepo, IFileRepo fileRepo)
        {
            _EvolutionRepo = evolutionRepo;
            _FileRepo = fileRepo;
        }

        public int Run(IEvolutionCreatable newEvolution)
        {
            try
            {
                var evolution = new Model.Evolution(evolutionName: newEvolution.TargetEvolution, created: DateTime.Now);

                _FileRepo.CreateEvolutionFile(evolution, newEvolution.SourceFile);

                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(GetExceptions(ex));
                return 1;
            }
        }

        //public int Run(CheckPointOptions options)
        //{
        //    throw new NotImplementedException();
        //}

        public int Run(IEvolutionTargetable targetedEvolution)
        {
            //TODO Add logic to run up to a specific evolution
            try
            {
                var executedEvolutions = _EvolutionRepo.GetExecutedEvolutionFileNames();
                var unexecutedEvolutionFiles = _FileRepo.GetUnexecutedEvolutionFiles(executedEvolutions);

                string fileContents;
                Model.Evolution evolution;

                foreach (var evolutionFile in unexecutedEvolutionFiles)
                {
                    evolution = new Model.Evolution(evolutionFile);

                    fileContents = _FileRepo.GetEvolutionFileContent(evolution);
                    _EvolutionRepo.ExecuteEvolution(fileContents);
                    _EvolutionRepo.AddEvolution(evolution, fileContents);
                }

                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(GetExceptions(ex));
                return 1;
            }
        }

        private string GetExceptions(Exception ex)
        {
            if (ex == null)
            {
                return string.Empty;
            }

            return string.Format("{0} \n {1} \n\n, {2}", ex.Message, ex.StackTrace, GetExceptions(ex.InnerException));
        }
    }
}
