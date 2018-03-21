using Evolution.Data;
using System.Collections.Generic;
using System.Linq;

namespace Evolution.Repo
{
    public class FileRepo : IFileRepo
    {
        private readonly IFileContext _Context;

        public FileRepo(IFileContext context)
        {
            _Context = context;
        }

        public void CreateEvolutionFile(string evolutionName, string sourceFile)
        {
            var fileName = string.Format("{0}.evo.sql", evolutionName);
            var contents = _Context.ReadFile(sourceFile);
            _Context.CreateFile(fileName, contents);
        }

        public string GetEvolutionFileContent(string fileName)
        {
            return _Context.GetEvolutionFileContent(fileName);
        }

        public IEnumerable<string> GetUnexecutedEvolutionFiles(string[] executedEvolutions)
        {
            var evolutionFileNames = _Context.GetEvolutionFileNames();

            //Use list of executed evolutions to get the list of unexecuted evolutions
            return evolutionFileNames.Except(executedEvolutions);
        }
    }
}
