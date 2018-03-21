using Ardalis.GuardClauses;
using System;

namespace Evolution.Model
{
    public class Evolution
    {
        public string FileName { get; }
        public string Name { get; }
        public IDate Created { get; }

        public Evolution(IDate created, string evolutionName) :
            this(evolutionName, created) { }

        public Evolution(string evolutionName, IDate created, string fileName = null)
        {
            Guard.Against.Null(created, nameof(created));
            Guard.Against.NullOrWhiteSpace(evolutionName, nameof(evolutionName));

            if (string.IsNullOrEmpty(fileName))
                fileName = CreateFileName(created, evolutionName);
            else
                Guard.Against.NullOrWhiteSpace(fileName, nameof(fileName));

            Name = evolutionName;
            FileName = fileName;
            Created = created;
        }

        public void Create(string sourceFile)
        {
            throw new NotImplementedException();
        }

        private static string CreateFileName(IDate created, string evolutionName)
        {
            return string.Format("{0}_{1}.evo.sql", created.ToString(), evolutionName);
        }
    }
}
