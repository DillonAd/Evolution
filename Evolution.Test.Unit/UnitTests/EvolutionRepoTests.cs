using Evolution.Data.Entity;
using Evolution.Repo;
using Evolution.Test.UnitTests.Infrastructure;
using System;
using System.Collections.Generic;
using Xunit;

namespace Evolution.Test.Unit
{
    public class EvolutionRepoTests
    {
        [Fact]
        [Trait("Category", "unit")]
        public void GetExecutedEvolutions()
        {
            var evolutions = new List<IEvolution>()
            {
                new Data.Entity.Evolution() { Name = "Evolution1" },
                new Data.Entity.Evolution() { Name = "Evolution2" },
                new Data.Entity.Evolution() { Name = "Evolution3" }
            };

            var context = new EvolutionContextBuilder()
                .AddGetEvolutionBehavior(evolutions)
                .Context;

            var repo = new EvolutionRepo(context);
            var executedEvolutions = repo.GetExecutedEvolutionFileNames();

            Assert.Equal(evolutions.Count, executedEvolutions.Length);
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

        [Fact]
        [Trait("Category", "unit")]
        public void ExecuteEvolution_Success()
        {
            var success = false;
            const string evolutionContent = "select sysdate from dual;";

            var context = new EvolutionContextBuilder()
                .AddGetEvolutionBehavior(new List<IEvolution>())
                .Context;

            var repo = new EvolutionRepo(context);

            try
            {
                repo.ExecuteEvolution(evolutionContent);
                success = true;
            }
            catch (Exception)
            {
                success = false;
            }

            Assert.True(success);
        }
    }
}
