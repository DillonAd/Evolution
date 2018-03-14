using Evolution.Data;
using Evolution.Model;
using Evolution.Repo;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Xunit;

namespace Evolution.Test.Unit
{
    public class EvolutionRepoTests
    {
        [Fact]
        public void GetExecutedEvolutions()
        {
            var evolutions = new List<IProgression>()
            {
                new Progression() { Name = "Evolution1" },
                new Progression() { Name = "Evolution2" },
                new Progression() { Name = "Evolution3" }
            };

            var context = SetupEvolutionContext(evolutions);

            var repo = new EvolutionRepo(context);
            var executedEvolutions = repo.GetExecutedEvolutions();

            Assert.Equal(evolutions.Count, executedEvolutions.Length);
        }

        //TODO Create Tests for Checkpoints
        //[Fact]
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

        [Fact]
        public void AddEvolution()
        {
            var evolution = new Model.Progression() { Name = "Evolution1" };

            var evolutions = new List<IProgression>();
            var context = SetupEvolutionContext(evolutions);
            var repo = new EvolutionRepo(context);
            
            repo.AddEvolution(evolution);

            Assert.Contains(evolution, evolutions);
        }

        [Fact]
        public void ExecuteEvolution_Success()
        {
            var success = false;
            const string evolutionContent = "select sysdate from dual;";

            var context = SetupEvolutionContext(new List<IProgression>());
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

        private IEvolutionContext SetupEvolutionContext(List<IProgression> evolutions)
        {
            var queryableEvolutions = evolutions.AsQueryable();
            
            var dbSetMock = new Mock<DbSet<IProgression>>();
            dbSetMock.As<IQueryable<IProgression>>().Setup(e => e.Provider).Returns(queryableEvolutions.Provider);
            dbSetMock.As<IQueryable<IProgression>>().Setup(e => e.Expression).Returns(queryableEvolutions.Expression);
            dbSetMock.As<IQueryable<IProgression>>().Setup(e => e.ElementType).Returns(queryableEvolutions.ElementType);
            dbSetMock.As<IQueryable<IProgression>>().Setup(e => e.GetEnumerator()).Returns(() => queryableEvolutions.GetEnumerator());
            dbSetMock.Setup(d => d.Add(It.IsAny<IProgression>())).Callback<IProgression>(e => evolutions.Add(e));
            dbSetMock.Setup(d => d.Remove(It.IsAny<IProgression>())).Callback<IProgression>(e => evolutions.Remove(e));

            var contextMock = new Mock<IEvolutionContext>();
            contextMock.Setup(cm => cm.Evolutions).Returns(dbSetMock.Object);

            return contextMock.Object;
        }
    }
}
