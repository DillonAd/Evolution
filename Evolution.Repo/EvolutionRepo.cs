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

        //TODO change return type to string[] to preserve space for longer histories
        public IProgression[] GetExecutedEvolutions()
        {
            var evolutions = new List<IProgression>();

            //TODO add logic to get all evolutions from the last checkpoint   
            foreach (var evolution in _Context.Evolutions)
            {
                evolutions.Add(evolution);
            }

            return evolutions.ToArray();
        }

        //TODO Add ability to create checkpoint
        public void CreateEvolutionCheckPoint(Guid evolutionId)
        {
            
        }

        public void AddEvolution(IProgression evolution)
        {
            _Context.Evolutions.Add(evolution);
        }

        //TODO Possibly not neccessary
        public void RemoveEvolution(IProgression evolution)
        {
            _Context.Evolutions.Remove(evolution);
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
