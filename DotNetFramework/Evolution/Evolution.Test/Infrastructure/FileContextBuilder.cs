using Evolution.Data;
using Evolution.Exceptions;
using Moq;
using System.Linq;
using System.Collections.Generic;

namespace Evolution.Test.Infrastructure
{
    public class FileContextBuilder
    {
        private readonly Mock<IFileContext> _ContextMock;
        public List<KeyValuePair<string, string>> _ContextFiles { get; }

        public int FileCount => _ContextFiles.Count;

        public FileContextBuilder()
        {
            _ContextMock = new Mock<IFileContext>();
        }

        public void AddFileInfo(string fileName, string content)
        {
            _ContextFiles.Add(new KeyValuePair<string, string>(fileName, content));
        }

        public void AddCreateFileBehavior()
        {
            _ContextMock.Setup(c => c.CreateFile(It.IsAny<string>(), It.IsAny<string>())).Callback<string, string>((fileName, content) =>
            {
                if (_ContextFiles.Contains(new KeyValuePair<string, string>(fileName, content)))
                {
                    throw new EvolutionFileException("Evolution file already exists");
                }
                else
                {
                    _ContextFiles.Add(new KeyValuePair<string, string>(fileName, content));
                }
            });
        }

        public void AddFileDeleteBehavior()
        {
            _ContextMock.Setup(c => c.DeleteFile(It.IsAny<string>()))
                .Callback<string>(fileName => _ContextFiles.Remove(_ContextFiles.Find(f => f.Key == fileName)));
        }

        public IFileContext GetFileContext()
        {
            _ContextMock.Setup(c => c.CreateFile(It.IsAny<string>(), It.IsAny<string>())).Callback<string, string>((fileName, fileContents) => _ContextFiles.Add(new KeyValuePair<string, string>(fileName, fileContents)));
            return _ContextMock.Object;
        }
    }
}
