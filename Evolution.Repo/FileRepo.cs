using Evolution.Data;
using Evolution.Domain;
using Evolution.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Evolution.Repo
{
    public class FileRepo : IFileRepo
    {
        private readonly IFileContext _Context;

        public FileRepo(IFileContext context)
        {
            _Context = context;
        }
        
        public void CreateMigrationFiles(string migrationName)
        {
            string[] fileNames = new string[]
            {
                string.Format("{0}.{1}.sql", migrationName, "up"),
                string.Format("{0}.{1}.sql", migrationName, "down")
            };

            try
            {
                foreach (string fileName in fileNames)
                {
                    _Context.CreateFile(fileName);
                }
            }
            catch(MigrationFileException)
            {
                foreach (string fileName in fileNames)
                {
                    _Context.DeleteFile(fileName);
                }

                throw;
            }
        }

        public string GetMigrationFileContent(string fileName)
        {
            return _Context.GetMigrationFileContent(fileName);
        }

        public IEnumerable<string> GetUnexecutedMigrations(Migration[] executedMigrations)
        {
            var migrationFileNames = _Context.GetMigrationFileNames();
            var executedMigrationFiles = new List<string>();

            foreach (Migration migration in executedMigrations)
            {
                executedMigrationFiles.AddRange(migrationFileNames.Where(m => 
                    Regex.IsMatch(m, migration.Name + ".[A-Za-z]{2,4}.sql")));
            }
            
            return migrationFileNames.Where(m => !executedMigrationFiles.Contains(m));
        }
    }
}
