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

        public void CreateEvolutionFile(Model.Evolution evolution, string sourceFile)
        {
            var contents = _Context.ReadFile(sourceFile);
            _Context.CreateFile(evolution.FileName, contents);
        }

        public string GetEvolutionFileContent(Model.Evolution evolution)
        {
            return _Context.GetEvolutionFileContent(evolution.FileName);
        }

        public IEnumerable<string> GetUnexecutedEvolutionFiles(string[] executedEvolutions)
        {
            var evolutionFileNames = _Context.GetEvolutionFileNames();

            //Use list of executed evolutions to get the list of unexecuted evolutions
            return evolutionFileNames.Except(executedEvolutions);
        }
    }
}
