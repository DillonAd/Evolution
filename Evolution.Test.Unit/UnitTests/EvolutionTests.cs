using Evolution.Exceptions;
using System;
using Xunit;

namespace Evolution.Test.Unit
{
    public class EvolutionTests
    {
        [Theory]
        [Trait("Category", "unit")]
        [InlineData("initialize")]
        [InlineData("addTable")]
        [InlineData("add2Tables")]
        [InlineData("123456789")]
        [InlineData("a")]
        public void Evolution_ValidEvolutionName(string evolutionName)
        {
            var evolution = new Model.Evolution("date_" + evolutionName);
            Assert.Equal(evolutionName, evolution.Name);
        }

        [Theory]
        [Trait("Category", "unit")]
        [InlineData("")]
        [InlineData("   ")]
        public void Evolution_InvalidEvolutionName(string evolutionName)
        {
            Assert.Throws<ArgumentException>(() => new Model.Evolution(evolutionName));
        }

        [Fact]
        [Trait("Category", "unit")]
        public void Evolution_NullEvolutionName()
        {
            Assert.Throws<ArgumentNullException>(() => new Model.Evolution(null));
        }

        [Theory]
        [Trait("Category", "unit")]
        [InlineData("initialize")]
        [InlineData("addTable")]
        [InlineData("add2Tables")]
        [InlineData("123456789")]
        [InlineData("a")]
        public void Evolution_ValidFileNameFromEvolutionName(string evolutionName)
        {
            var evolution = new Model.Evolution(evolutionName, DateTime.Now);
            Assert.Matches(@"[0-9]{14}_\w{1,}.evo.sql", evolution.FileName);
        }

        [Theory]
        [Trait("Category", "unit")]
        [InlineData("20181213145432_evolution1.evo.sql")]
        public void Evolution_ValidFileName(string fileName)
        {
            var evolution = new Model.Evolution(fileName);

            Assert.NotNull(evolution.Name);
            Assert.False(string.IsNullOrWhiteSpace(fileName));
        }

        [Theory]
        [Trait("Category", "unit")]
        [InlineData("evolution1.evo.sql")]
        [InlineData("evolution1.sql")]
        [InlineData("evolution1.evo")]
        [InlineData("20181213145432evolution1.evo.sql")]
        public void Evolution_InvalidFileName(string fileName)
        {
            Assert.Throws<EvolutionException>(() => new Model.Evolution(fileName));
        }
    }
}
