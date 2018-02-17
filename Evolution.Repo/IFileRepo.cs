namespace Evolution.Repo
{
    public interface IFileRepo
    {
        void CreateMigrationFiles(string migrationName);
    }
}
