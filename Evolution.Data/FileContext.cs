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
        }
    }
}
