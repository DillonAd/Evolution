using Ardalis.GuardClauses;
using Evolution.Exceptions;
using System;

namespace Evolution.Model
{
    public class Evolution
    {
        public string FileName { get; }
        public string Name { get; }
        public DateTime Created { get; }

        public Evolution(string evolutionName, DateTime created)
        {
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

        private string CreateFileName(DateTime created, string evolutionName)
        {
            return string.Format("{0}{1}{2}{3}{4}{5}_{6}.evo.sql", created.Year.ToString(),
                                                        created.Month.ToString("00"),
                                                        created.Day.ToString("00"),
                                                        created.ToString("HH"),
                                                        created.Minute.ToString("00"),
                                                        created.Second.ToString("00"),
                                                        evolutionName);
        }

        private string ParseEvolutionName(string fileName)
        {
            try
            {
                fileName = fileName.Replace(".evo.sql", string.Empty);

                var firstUnderscoreIndex = fileName.IndexOf('_');

                if (firstUnderscoreIndex < 0)
                {
                    throw new ArgumentOutOfRangeException("Cant find underscore for in proper file name format.");
                }

                return fileName.Substring(firstUnderscoreIndex + 1);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw new EvolutionException("Error parsing file name.", ex);
            }
        }
    }
}
