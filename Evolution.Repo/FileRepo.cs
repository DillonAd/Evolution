using Evolution.Data;

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
            string fileName = string.Format("{0}.{1}.sql", migrationName, "up");
            _Context.CreateFile(fileName);

            fileName = string.Format("{0}.{1}.sql", migrationName, "down");
            _Context.CreateFile(fileName);
        }
    }
}
