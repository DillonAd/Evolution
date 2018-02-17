using Evolution.Domain;
using System.Collections.Generic;

namespace Evolution.Repo
{
    public interface IFileRepo
    {
        void CreateMigrationFiles(string migrationName);
        string GetMigrationFileContent(string fileName);
        IEnumerable<string> GetUnexecutedMigrations(Migration[] executedMigrations);
    }
}
