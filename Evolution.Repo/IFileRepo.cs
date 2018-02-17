namespace Evolution.Repo
{
    public interface IFileRepo
    {
        void CreateMigrationFiles(string migrationName);
        string GetMigrationFileContent(string fileName);
    }
}
