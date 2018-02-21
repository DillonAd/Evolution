using Evolution.Exceptions;
using System.IO;

namespace Evolution.Data
{
    public class FileContext : IFileContext
    {
        public void CreateFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                File.Create(fileName);
            }
            else
            {
                throw new EvolutionFileException("Evolution file already exists : " + fileName);
            }
        }

        public void DeleteFile(string fileName)
        {
            File.Delete(fileName);
        }

        public string GetEvolutionFileContent(string fileName)
        {
            if(File.Exists(fileName))
            {
                return File.ReadAllText(fileName);
            }

            return string.Empty;
        }

        public string[] GetEvolutionFileNames()
        {
            return Directory.GetFiles("./", "*.evo.sql");
        }
    }
}
