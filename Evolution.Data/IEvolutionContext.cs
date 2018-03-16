using Evolution.Data.Entity;
using System.Data.Entity;

namespace Evolution.Data
{
    public interface IEvolutionContext
    {
        DbSet<IEvolution> Evolutions { get; set; }
        int SaveChanges();
        void ExecuteEvolution(string content);
    }
}
