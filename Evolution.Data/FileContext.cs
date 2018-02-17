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
                throw new MigrationFileException("Migration file already exists : " + fileName);
            }
        }

        public void DeleteFile(string fileName)
        {
            File.Delete(fileName);
        }

        public string GetMigrationFileContent(string fileName)
        {
            if(File.Exists(fileName))
            {
                return File.ReadAllText(fileName);
            }

            return string.Empty;
        }

        public string[] GetMigrationFileNames()
        {
            return Directory.GetFiles("./", "*.sql");
        }
    }
}
