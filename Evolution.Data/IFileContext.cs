namespace Evolution.Data
{
    public interface IFileContext
    {
        void CreateFile(string fileName, string contents);
        void DeleteFile(string fileName);
        string GetEvolutionFileContent(string fileName);
        string[] GetEvolutionFileNames();
        string ReadFile(string fileName);
    }
}
