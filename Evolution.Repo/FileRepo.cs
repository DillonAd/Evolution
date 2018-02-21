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
            return _Context.GetMigrationFileContent(fileName);
        }

        public IEnumerable<IProgression> GetUnexecutedEvolutions(IProgression[] executedEvolutions)
        {
            var evolutionFileNames = _Context.GetMigrationFileNames();
            var executedMigrationFiles = new List<string>();

            //Get executed evolutions
            foreach (IProgression evolution in executedEvolutions)
            {
                executedMigrationFiles.AddRange(evolutionFileNames.Where(m =>
                    Regex.IsMatch(m, evolution.Name + ".[A-Za-z]{2,4}.sql")));
            }
            
            //Use list of executed evolutions to get the list of unexecuted migrations
            var unexecutedEvolutionFiles = evolutionFileNames.Where(m => !executedMigrationFiles.Contains(m));

            var unexecutedEvolutions = new List<IProgression>();

            int suffixIdx;
            string evolutionName;

            foreach(var unexecutedEvolutionFile in unexecutedEvolutionFiles)
            {
                suffixIdx = unexecutedEvolutionFile.LastIndexOf(".evo.sql");
                evolutionName = unexecutedEvolutionFile.Substring(0, unexecutedEvolutionFile.Length - suffixIdx);
                unexecutedEvolutions.Add(new Progression() { Name = evolutionName});
            }

            return unexecutedEvolutions;
        }
    }
}
