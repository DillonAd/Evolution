using Evolution.Data;
using Evolution.Exceptions;
using Evolution.Model;
using Evolution.Repo;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Evolution.Test.Unit
{
    public class FileRepoTests
    {
        [Fact]
        public void CreateEvolutionFile_Success()
        {
            var evolutionName = "Evolution1";
            var fileList = new List<string>();

            var mockContext = new Mock<IFileContext>();
            mockContext.Setup(c => c.CreateFile(It.IsAny<string>())).Callback<string>(fileName => fileList.Add(fileName));

            var repo = new FileRepo(mockContext.Object);
            repo.CreateEvolutionFile(evolutionName);

            Assert.NotEmpty(fileList);
            Assert.Contains(evolutionName, fileList[0]);
        }

        [Fact]
        public void CreateEvolutionFiles_FileExists()
        {
            var evolutionName = "Evolution1";
            var fileList = new List<string>();
            var count = 0;

            var mockContext = new Mock<IFileContext>();
            mockContext.Setup(c => c.CreateFile(It.IsAny<string>())).Callback<string>((fileName) =>
            {
                count++;

                if (count > 1)
                {
                    throw new EvolutionFileException("Evolution file already exists");
                }
                else
                {
                    fileList.Add(fileName);
                }
            });
            mockContext.Setup(c => c.DeleteFile(It.IsAny<string>())).Callback<string>(fileName => fileList.Remove(fileName));

            var repo = new FileRepo(mockContext.Object);

            Assert.Throws<EvolutionFileException>(() => repo.CreateEvolutionFile(evolutionName));
            Assert.Empty(fileList);
        }

        [Fact]
        public void GetEvolutionFileContents()
        {
            var fileName = "evolution1.up.sql";
            var content = "evolution file content";

            var mockContext = new Mock<IFileContext>();
            mockContext.Setup(c => c.GetEvolutionFileContent(It.Is<string>(f => f == fileName))).Returns(content);

            var repo = new FileRepo(mockContext.Object);
            var contentResult = repo.GetEvolutionFileContent(fileName);

            Assert.NotNull(contentResult);
            Assert.NotEmpty(contentResult);
            Assert.Equal(content, contentResult);
        }

        [Fact]
        public void GetUnexecutedEvolutions()
        {
            var executedEvolutions = new IProgression[]
            {
                new Progression() { Name = "Evolution1", FileName="Evolution1.evo.sql" },
                new Progression() { Name = "Evolution2", FileName="Evolution2.evo.sql" },
                new Progression() { Name = "Evolution3", FileName="Evolution3.evo.sql" }
            };

            var unexecutedEvolutionFiles = new string[]
            {
                "Evolution4.evo.sql",
                "Evolution5.evo.sql",
                "Evolution6.evo.sql"
            };

            var evolutionFileNames = new List<string>()
            {
                "Evolution1.evo.sql",
                "Evolution2.evo.sql",
                "Evolution3.evo.sql",
            };
            evolutionFileNames.AddRange(unexecutedEvolutionFiles);

            var mockContext = new Mock<IFileContext>();
            mockContext.Setup(mc => mc.GetEvolutionFileNames()).Returns(evolutionFileNames.ToArray());

            var repo = new FileRepo(mockContext.Object);
            var unexecutedEvolutions = repo.GetUnexecutedEvolutionFiles(executedEvolutions).ToArray();

            Assert.NotEmpty(unexecutedEvolutions);
            Assert.Equal(unexecutedEvolutionFiles.Length, unexecutedEvolutions.Length);
            Assert.All(unexecutedEvolutions, e => unexecutedEvolutionFiles.Contains(e));
        }
    }
}
