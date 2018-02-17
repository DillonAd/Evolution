using Evolution.Data;
using Evolution.Exceptions;

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
    }
}
