using Evolution.Data;
using Evolution.Model;
using System.Collections.Generic;

namespace Evolution.Repo
{
    public class EvolutionRepo : IEvolutionRepo
    {
        private readonly IEvolutionContext _Context;

        public EvolutionRepo(IEvolutionContext context)
        {
            _Context = context;
        }

        public IProgression[] GetExecutedEvolutions()
        {
            var evolutions = new List<IProgression>();

            foreach(var evolution in _Context.Evolutions)
            {
                evolutions.Add(evolution);
            }

            return evolutions.ToArray();
        }

        public void AddEvolution(IProgression evolution)
        {
            _Context.Evolutions.Add(evolution);
        }

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
