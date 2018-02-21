using Evolution.Repo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Evolution.Domain
{
    public class EvolutionManager
    {
        private readonly IEvolutionRepo _MigrationRepo;
        private readonly IFileRepo _FileRepo;

        public EvolutionManager(IEvolutionRepo migrationRepo, IFileRepo fileRepo)
        {
            _MigrationRepo = migrationRepo;
            _FileRepo = fileRepo;
        }

        public void Evolve()
        {
            var executedEvolutions = _MigrationRepo.GetExecutedEvolutions();
            var unexecutedEvolutions = _FileRepo.GetUnexecutedEvolutions(executedEvolutions);

            foreach(var evolution in unexecutedEvolutions)
            {
                
            }
        }
    }
}
