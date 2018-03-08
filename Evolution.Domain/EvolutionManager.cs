using Evolution.Model;
using Evolution.Repo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Evolution.Domain
{
    public class EvolutionManager
    {
        private readonly IEvolutionRepo _EvolutionRepo;
        private readonly IFileRepo _FileRepo;

        public EvolutionManager(IEvolutionRepo evolutionRepo, IFileRepo fileRepo)
        {
            _EvolutionRepo = evolutionRepo;
            _FileRepo = fileRepo;
        }

        public void Evolve()
        {
            var executedEvolutions = _EvolutionRepo.GetExecutedEvolutions();
            var unexecutedEvolutionFiles = _FileRepo.GetUnexecutedEvolutionFiles(executedEvolutions);
            string evolutionContent;

            foreach(var evolutionFile in unexecutedEvolutionFiles)
            {
                evolutionContent = _FileRepo.GetEvolutionFileContent(evolutionFile);
                _EvolutionRepo.ExecuteEvolution(evolutionContent);
                _EvolutionRepo.AddEvolution(
                    new Progression()
                    {
                        Name = evolutionFile.Replace(".evo.sql", ""),
                        FileName = evolutionFile
                    }
                );
            }
        }
    }
}
