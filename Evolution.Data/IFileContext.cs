namespace Evolution.Data
{
    public interface IFileContext
    {
        void CreateFile(string fileName);
        void DeleteFile(string fileName);
        string GetEvolutionFileContent(string fileName);
        string[] GetEvolutionFileNames();
    }
}
