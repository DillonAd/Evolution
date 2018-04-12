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

        public void AddEvolution(Model.Evolution evolution, string content)
        {
            var newEvolution = new Data.Entity.Evolution()
            {
                Id = Guid.NewGuid(),
                Name = evolution.Name,
                FileName = evolution.FileName,
                Content = content
            };

            _Context.Evolutions.Add(newEvolution);
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
