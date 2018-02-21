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

            foreach(var evolution in unexecutedEvolutions)
            {
                
            }
        }
    }
}
