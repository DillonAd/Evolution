using Evolution.Domain;
using System.Collections.Generic;

namespace Evolution.Repo
{
    public interface IMigrationRepo
    {
        IEnumerable<IMigration> GetExecutedMigrations();
        void AddMigration(IMigration migration);
        void RemoveMigration(IMigration migration);
        void SaveChanges();
        void ExecuteMigration(string content);
    }
}
