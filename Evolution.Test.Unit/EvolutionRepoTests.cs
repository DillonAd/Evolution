﻿using Evolution.Data.Entity;
using Evolution.Repo;
using Evolution.Test.Unit.Infrastructure;
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
            //Arrange
            var evolutions = new List<IEvolution>()
            {
                new Data.Entity.Evolution() { Name = "Evolution1" },
                new Data.Entity.Evolution() { Name = "Evolution2" },
                new Data.Entity.Evolution() { Name = "Evolution3" }
            };

            var context = new EvolutionContextBuilder()
                .AddGetEvolutionBehavior(evolutions)
                .Context;

            //Act
            var repo = new EvolutionRepo(context);
            var executedEvolutions = repo.GetExecutedEvolutionFileNames();

            //Assert
            Assert.Equal(evolutions.Count, executedEvolutions.Length);
        }

        [Fact]
        [Trait("Category", "unit")]
        public void ExecuteEvolution_Success()
        {
            //Arrange
            var success = false;
            const string evolutionContent = "select sysdate from dual;";

            var context = new EvolutionContextBuilder()
                .AddGetEvolutionBehavior(new List<IEvolution>())
                .Context;

             //Act
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

            //Assert
            Assert.True(success);
        }
    }
}
