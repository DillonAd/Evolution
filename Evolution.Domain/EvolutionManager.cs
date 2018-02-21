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
            var unexecutedEvolutions = _FileRepo.GetUnexecutedEvolutions(executedEvolutions);
            string evolutionContent;

            foreach(var evolution in unexecutedEvolutions)
            {
                evolutionContent = _FileRepo.GetEvolutionFileContent(evolution.FileName);
                _EvolutionRepo.ExecuteEvolution(evolutionContent);
                _EvolutionRepo.AddEvolution(evolution);
            }
        }
    }
}
