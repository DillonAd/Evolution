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
            var migrations = new List<IProgression>()
            {
                new Progression() { Name = "Migration1" },
                new Progression() { Name = "Migration2" },
                new Progression() { Name = "Migration3" }
            };

            var context = SetupEvolutionContext(migrations);

            var repo = new EvolutionRepo(context);
            var executedMigrations = repo.GetExecutedEvolutions();

            Assert.Equal(migrations.Count, executedMigrations.Length);
        }

        [Fact]
        public void AddEvolution()
        {
            var migration = new Model.Progression() { Name = "Migration1" };

            var migrations = new List<IProgression>();
            var context = SetupEvolutionContext(migrations);
            var repo = new EvolutionRepo(context);
            
            repo.AddEvolution(migration);

            Assert.Contains(migration, migrations);
        }

        [Fact]
        public void RemoveEvolution()
        {
            var evolution = new Progression() { Name = "Migration1" };

            var evolutions = new List<IProgression>()
            {
                evolution
            };

            var context = SetupEvolutionContext(evolutions);
            var repo = new EvolutionRepo(context);

            repo.RemoveEvolution(evolution);

            Assert.Empty(evolutions);
        }

        [Fact]
        public void ExecuteMigration_Success()
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
