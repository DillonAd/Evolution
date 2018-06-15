using Evolution.Data.Entity;
using System;
using System.Collections.Generic;

namespace Evolution.Data
{
    public interface IEvolutionContext : IDisposable
    {
        void AddEvolution(IEvolution evolution);
        void ExecuteEvolution(string content);
        List<IEvolution> GetEvolutions();
    }
}
