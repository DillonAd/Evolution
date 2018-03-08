using Evolution.Data;
using Evolution.Exceptions;
using Evolution.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Evolution.Repo
{
    public class FileRepo : IFileRepo
    {
        private readonly IFileContext _Context;

        public FileRepo(IFileContext context)
        {
            _Context = context;
        }
        
        public void CreateEvolutionFile(string evolutionName)
        {
            string[] fileNames = new string[]
            {
                string.Format("{0}.{1}.sql", evolutionName, "up"),
                string.Format("{0}.{1}.sql", evolutionName, "down")
            };

            try
            {
                foreach (string fileName in fileNames)
                {
                    _Context.CreateFile(fileName);
                }
            }
            catch(EvolutionFileException)
            {
                foreach (string fileName in fileNames)
                {
                    _Context.DeleteFile(fileName);
                }

                throw;
            }
        }

        public string GetEvolutionFileContent(string fileName)
        {
            return _Context.GetEvolutionFileContent(fileName);
        }

        public IEnumerable<string> GetUnexecutedEvolutionFiles(IProgression[] executedEvolutions)
        {
            var evolutionFileNames = _Context.GetEvolutionFileNames();

            //Use list of executed evolutions to get the list of unexecuted evolutions
            return evolutionFileNames.Except(executedEvolutions.Select(e => e.FileName));
        }
    }
}
