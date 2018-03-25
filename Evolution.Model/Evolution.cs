using Ardalis.GuardClauses;
using System;

namespace Evolution.Model
{
    public class Evolution
    {
        public string FileName { get; }
        public string Name { get; }
        public IDate Created { get; }

        public Evolution(IDate created, string evolutionName)
        {
            Guard.Against.Null(created, nameof(created));
            Guard.Against.NullOrWhiteSpace(evolutionName, nameof(evolutionName));

            Name = evolutionName;
            FileName = CreateFileName(created, evolutionName);
            Created = created;
        }

        public Evolution(string fileName)
        {
            Guard.Against.NullOrWhiteSpace(fileName, nameof(fileName));

            Name = ParseEvolutionName(fileName);
            FileName = fileName;
        }

        private string CreateFileName(IDate created, string evolutionName)
        {
            return string.Format("{0}_{1}.evo.sql", created.ToString(), evolutionName);
        }

        private string ParseEvolutionName(string fileName)
        {
            try
            {
                fileName.Replace(".evo.sql", string.Empty);

                var firstUnderscoreIndex = fileName.IndexOf('_');
                return fileName.Substring(firstUnderscoreIndex);
            }
            catch(ArgumentOutOfRangeException ex)
            {
                throw new ArgumentException("Error parsing file name.", ex);
            }
        }
    }
}
