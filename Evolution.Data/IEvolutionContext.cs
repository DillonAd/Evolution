using Evolution.Data.Entity;
using System.Collections.Generic;

namespace Evolution.Data
{
    public interface IEvolutionContext
    {
        void AddEvolution(IEvolution evolution);
        void ExecuteEvolution(string content);
        List<IEvolution> GetEvolutions();
    }
}
