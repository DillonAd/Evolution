using Evolution.Data;
using Evolution.Exceptions;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace Evolution.Test.Unit.Infrastructure
{
    public class FileContextBuilder
    {
        public IFileContext Context => _Mock.Object;
        public int EvolutionCount => _Evolutions.Count;

        private readonly Mock<IFileContext> _Mock;
        private readonly List<KeyValuePair<string, string>> _Evolutions;
        private readonly List<KeyValuePair<string, string>> _SourceFiles;

        public FileContextBuilder()
        {
            _Mock = new Mock<IFileContext>();
            _Evolutions = new List<KeyValuePair<string, string>>();
            _SourceFiles = new List<KeyValuePair<string, string>>();
        }

        public FileContextBuilder AddEvolution(string fileName, string content)
        {
            _Evolutions.Add(new KeyValuePair<string, string>(fileName, content));
            
            return this;
        }

        public FileContextBuilder AddSourceFile(string fileName, string content)
        {
            _SourceFiles.Add(new KeyValuePair<string, string>(fileName, content));    

            return this;
        }

        public FileContextBuilder AddCreateEvolutionFileBehavior()
        {
            _Mock.Setup(c => c.CreateFile(It.IsAny<string>(), It.IsAny<string>()))
                .Callback<string, string>((fileName, fileContents) =>
                {
                    if (_Evolutions.Any(e => e.Key == fileName))
                    {
                        throw new EvolutionFileException("Evolution file already exists");
                    }
                    else
                    {
                        AddEvolution(fileName, fileContents);
                    }
                });

            return this;
        }

        public FileContextBuilder AddGetEvolutionFileNamesBehavior(string[] evolutionFileNames)
        {
            _Mock.Setup(mc => mc.GetEvolutionFileNames()).Returns(evolutionFileNames);

            return this;
        }

        public FileContextBuilder AddGetEvolutionFileContentBehavior()
        {
            _Mock.Setup(c => c.GetEvolutionFileContent(It.IsAny<string>())).Returns((string fileName) =>
                _Evolutions.FirstOrDefault(e => e.Key == fileName).Value);

            return this;
        }
    }
}
