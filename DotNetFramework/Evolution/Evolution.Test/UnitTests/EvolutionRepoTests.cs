using Evolution.Data;
using Evolution.Data.Entity;
using Evolution.Repo;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Evolution.Test.Unit
{
    public class EvolutionRepoTests
    {
        [Test]
        [Category("unit")]
        public void GetExecutedEvolutions()
        {
            var evolutions = new List<IEvolution>()
            {
                new Data.Entity.Evolution() { Name = "Evolution1" },
                new Data.Entity.Evolution() { Name = "Evolution2" },
                new Data.Entity.Evolution() { Name = "Evolution3" }
            };

            var context = SetupEvolutionContext(evolutions);

            var repo = new EvolutionRepo(context);
            var executedEvolutions = repo.GetExecutedEvolutionFileNames();

            Assert.AreEqual(evolutions.Count, executedEvolutions.Length);
        }

        //TODO Create Tests for Checkpoints
        //[Test]
        //[Category("unit")]
        //public void GetExecutedEvolutionsSinceCheckPoint()
        //{
        //    var evolutions = new List<IProgression>()
        //    {
        //        new Progression() { Name = "Evolution1" },
        //        new Progression() { Name = "Evolution2" },
        //        new Progression() { Name = "Evolution3" }
        //    };

        //    var context = SetupEvolutionContext(evolutions);

        //    var repo = new EvolutionRepo(context);
        //    var executedEvolutions = repo.GetExecutedEvolutions();

        //    Assert.Equal(evolutions.Count, executedEvolutions.Length);
        //}

        [Test]
        [Category("unit")]
        public void ExecuteEvolution_Success()
        {
            var success = false;
            const string evolutionContent = "select sysdate from dual;";

            var context = SetupEvolutionContext(new List<IEvolution>());
            var repo = new EvolutionRepo(context);

            try
            {
                repo.ExecuteEvolution(evolutionContent);
                success = true;
            }
            catch(Exception)
            {
                success = false;
            }

            Assert.True(success);
        }

        private IEvolutionContext SetupEvolutionContext(List<IEvolution> evolutions)
        {
            var contextMock = new Mock<IEvolutionContext>();
            contextMock.Setup(cm => cm.GetEvolutions()).Returns(evolutions);

            return contextMock.Object;
        }
    }
}
