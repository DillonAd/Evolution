using Evolution.Data;
using Evolution.Data.Entity;
using Evolution.Model;
using Evolution.Repo;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
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

            var context = SetupEvolutionContext(evolutions);

            var repo = new EvolutionRepo(context);
            var executedEvolutions = repo.GetExecutedEvolutionFileNames();

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
        [Trait("Category", "unit")]
        public void AddEvolution()
        {
            var evolution = new Model.Evolution(new Date(), "Evolution1");
            string content = "Evolution Content";

            var evolutions = new List<IEvolution>();
            var context = SetupEvolutionContext(evolutions);
            var repo = new EvolutionRepo(context);
            
            repo.AddEvolution(evolution, content);

            Assert.Single(evolutions);
        }

        [Fact]
        [Trait("Category", "unit")]
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
            return null;
        }
    }
}
