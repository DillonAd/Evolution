using Evolution.Model;
using System.Collections.Generic;

namespace Evolution.Repo
{
    public interface IFileRepo
    {
        void CreateEvolutionFile(string evolutionName);
        string GetEvolutionFileContent(string fileName);
        IEnumerable<IProgression> GetUnexecutedEvolutions(IProgression[] executedEvolutions);
    }
}
