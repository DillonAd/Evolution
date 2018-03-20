using System.Linq;

namespace Evolution.Model
{
    public class Evolution
    {
        public string FileName { get; }

        public string Name
        {
            get
            {
                return FileName.Replace(".evo.sql", string.Empty)
                    .Split('_')
                    .Last();
            }
        }

        public Evolution(IDate date, string fileName)
        {
            FileName = fileName;
        }

        public static string CreateFileName(IDate date, string evolutionName)
        {
            return string.Format("{0}_{1}.evo.sql", date.ToString(), evolutionName);
        }
    }
}
