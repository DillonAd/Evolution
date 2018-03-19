using Evolution.Data;
using Evolution.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Evolution.Repo
{
    public class EvolutionRepo : IEvolutionRepo
    {
        private readonly IEvolutionContext _Context;

        public EvolutionRepo(IEvolutionContext context)
        {
            _Context = context;
        }

        public string[] GetExecutedEvolutionFileNames()
        {
            //TODO add logic to get all evolutions from the last checkpoint   
            return _Context.Evolutions.Select(e => e.FileName).ToArray();
        }

        //TODO Add ability to create checkpoint
        public void CreateEvolutionCheckPoint(Guid evolutionId)
        {
            throw new NotImplementedException();   
        }

        public void AddEvolution(string name, string file, string content)
        {
            var evolution = new Data.Entity.Evolution()
            {
                Id = Guid.NewGuid()
            };

            _Context.Evolutions.Add(evolution);
        }

        public void SaveChanges()
        {
            _Context.SaveChanges();
        }

        public void ExecuteEvolution(string content)
        {
            _Context.ExecuteEvolution(content);
        }
    }
}
