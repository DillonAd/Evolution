using Evolution.Data;
using Evolution.Data.Entity;
using Evolution.Exceptions;
using Evolution.Model;
using Evolution.Repo;
using Evolution.Test.UnitTests.Infrastructure;
using Moq;
using NUnit.Framework;
using System;
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
            const string evolutionName = "Evolution1";
            const string evolutionContents = "Programatic awesomeness";

            var evolution = new Model.Evolution(evolutionName, DateTime.Now);
            
            var mockBuilder = new FileContextMockBuilder()
                .AddCreateEvolutionFileBehavior();

            var repo = new FileRepo(mockBuilder.Context);
            repo.CreateEvolutionFile(evolution, evolutionContents);

            Assert.NotZero(mockBuilder.EvolutionCount);
        }

        [Test]
        [Category("unit")]
        public void CreateEvolutionFiles_FileExists()
        {
            const string evolutionName = "Evolution1";
            const string evolutionContents = "Programatic awesomeness";

            var evolution = new Model.Evolution(evolutionName, DateTime.Now);
            
            var mockBuilder = new FileContextMockBuilder()
                .AddCreateEvolutionFileBehavior()
                .AddEvolution(evolution.FileName, evolutionContents);

            var repo = new FileRepo(mockBuilder.Context);

            Assert.Throws<EvolutionFileException>(() => repo.CreateEvolutionFile(evolution, evolutionContents));
            Assert.AreEqual(1, mockBuilder.EvolutionCount);
        }

        [Test]
        [Category("unit")]
        public void GetEvolutionFileContents()
        {
            const string fileName = "20180125131211_evolution1.up.sql";
            const string content = "evolution file content";
            
            var contextBuilder = new FileContextMockBuilder()
                .AddEvolution(fileName, content)
                .AddGetEvolutionFileContentBehavior();

            var repo = new FileRepo(contextBuilder.Context);
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
            
            var mockBuilder = new FileContextMockBuilder().AddGetEvolutionFileNamesBehavior(evolutionFileNames.ToArray());

            var repo = new FileRepo(mockBuilder.Context);
            var unexecutedEvolutions = repo.GetUnexecutedEvolutionFiles(executedEvolutions.Select(e => e.FileName).ToArray());

            CollectionAssert.IsNotEmpty(unexecutedEvolutions);
            Assert.AreEqual(unexecutedEvolutionFiles.Length, unexecutedEvolutions.Count());
            Assert.AreEqual(unexecutedEvolutionFiles, unexecutedEvolutions.ToArray());
        }
    }
}
