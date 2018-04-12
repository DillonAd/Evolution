using Evolution.Data.Entity;
using System.Collections.Generic;

namespace Evolution.Repo
{
    public interface IFileRepo
    {
        void CreateEvolutionFile(Model.Evolution evolution, string sourceFile);
        string GetEvolutionFileContent(Model.Evolution evolution);
        IEnumerable<string> GetUnexecutedEvolutionFiles(string[] executedEvolutions);
    }
}
