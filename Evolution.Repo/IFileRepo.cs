using Evolution.Data.Entity;
using System.Collections.Generic;

namespace Evolution.Repo
{
    public interface IFileRepo
    {
        void CreateEvolutionFile(string fileName, string sourceFile);
        string GetEvolutionFileContent(string fileName);
        IEnumerable<string> GetUnexecutedEvolutionFiles(string[] executedEvolutions);
    }
}
