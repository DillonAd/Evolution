using Evolution.Data;
using Evolution.Data.Entity;
using Evolution.Exceptions;
using Evolution.Model;
using Evolution.Repo;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Evolution.Test.Unit
{
    public class FileRepoTests
    {
        [Test]
        [Category("unit")]
        public void CreateEvolutionFile_Success()
        {
            var evolutionName = "Evolution1";
            var evolution = new Model.Evolution(new Date(), evolutionName);
            var evolutionContents = "Programatic awesomeness";
            var fileList = new List<string>();

            var mockContext = new Mock<IFileContext>();
            mockContext.Setup(c => c.CreateFile(It.IsAny<string>(), It.IsAny<string>())).Callback<string, string>((fileName, fileContents) => fileList.Add(fileName));

            var repo = new FileRepo(mockContext.Object);
            repo.CreateEvolutionFile(evolution, evolutionContents);

            CollectionAssert.IsNotEmpty(fileList);
            Assert.IsTrue(fileList[0].Contains(evolutionName));
        }

        [Test]
        [Category("unit")]
        public void CreateEvolutionFiles_FileExists()
        {
            var evolutionName = "Evolution1";
            var evolution = new Model.Evolution(new Date(), evolutionName);
            var evolutionContents = "Programatic awesomeness";
            var fileList = new List<string>();

            var mockContext = new Mock<IFileContext>();
            mockContext.Setup(c => c.CreateFile(It.IsAny<string>(), It.IsAny<string>())).Callback<string, string>((fileName, fileContents) =>
            {
                if (fileList.Contains(fileName))
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
            repo.CreateEvolutionFile(evolution, evolutionContents);

            Assert.Throws<EvolutionFileException>(() => repo.CreateEvolutionFile(evolution, evolutionContents));
            Assert.AreEqual(1, fileList.Count);
        }

        [Test]
        [Category("unit")]
        public void GetEvolutionFileContents()
        {
            var fileName = "20180125131211_evolution1.up.sql";
            var content = "evolution file content";

            var mockContext = new Mock<IFileContext>();
            mockContext.Setup(c => c.GetEvolutionFileContent(It.Is<string>(f => f == fileName))).Returns(content);

            var repo = new FileRepo(mockContext.Object);
            var contentResult = repo.GetEvolutionFileContent(new Model.Evolution(fileName));

            Assert.NotNull(contentResult);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(contentResult));
            Assert.AreEqual(content, contentResult);
        }

        [Test]
        [Category("unit")]
        public void GetUnexecutedEvolutions()
        {
            var executedEvolutions = new IEvolution[]
            {
                new Data.Entity.Evolution() { Name = "Evolution1", FileName="Evolution1.evo.sql" },
                new Data.Entity.Evolution() { Name = "Evolution2", FileName="Evolution2.evo.sql" },
                new Data.Entity.Evolution() { Name = "Evolution3", FileName="Evolution3.evo.sql" }
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
            var unexecutedEvolutions = repo.GetUnexecutedEvolutionFiles(executedEvolutions.Select(e => e.FileName).ToArray());

            CollectionAssert.IsNotEmpty(unexecutedEvolutions);
            Assert.AreEqual(unexecutedEvolutionFiles.Length, unexecutedEvolutions.Count());
            
            foreach(var unexecutedEvolutionFile in unexecutedEvolutionFiles)
            {
                CollectionAssert.Contains(unexecutedEvolutionFile, unexecutedEvolutions);
            }
        }
    }
}
