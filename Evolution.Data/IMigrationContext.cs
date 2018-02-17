using Evolution.Domain;
using System.Data.Entity;

namespace Evolution.Data
{
    public interface IMigrationContext
    {
        DbSet<IMigration> Migrations { get; set; }
        int SaveChanges();
        void ExecuteMigration(string content);
    }
}
