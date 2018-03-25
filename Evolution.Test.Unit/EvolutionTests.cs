using Evolution.Exceptions;
using Evolution.Model;
using Moq;
using System;
using Xunit;

namespace Evolution.Test.Unit
{
    public class EvolutionTests
    {
        [Theory]
        [InlineData("initialize")]
        [InlineData("addTable")]
        [InlineData("add2Tables")]
        [InlineData("123456789")]
        [InlineData("a")]
        public void Evolution_ValidEvolutionName(string evolutionName)
        {
            var date = new Date();
            var evolution = new Model.Evolution(date, evolutionName);
            Assert.Equal(evolutionName, evolution.Name);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void Evolution_InvalidEvolutionName(string evolutionName)
        {
            Assert.Throws<ArgumentException>(() => new Model.Evolution(new Date(), evolutionName));
        }

        [Fact]
        public void Evolution_NullEvolutionName()
        {
            Assert.Throws<ArgumentNullException>(() => new Model.Evolution(new Date(), null));
        }

        [Theory]
        [InlineData("initialize")]
        [InlineData("addTable")]
        [InlineData("add2Tables")]
        [InlineData("123456789")]
        [InlineData("a")]
        public void Evolution_ValidFileNameFromEvolutionName(string evolutionName)
        {
            var date = new Date();
            var evolution = new Model.Evolution(date, evolutionName);
            Assert.Matches(@"[0-9]{14}_\w{1,}.evo.sql", evolution.FileName);
        }

        [Theory]
        [InlineData("20181213145432_evolution1.evo.sql")]
        public void Evolution_ValidFileName(string fileName)
        {
            var evolution = new Model.Evolution(fileName);

            Assert.NotNull(evolution.Name);
            Assert.False(string.IsNullOrWhiteSpace(fileName));
        }

        [Theory]
        [InlineData("evolution1.evo.sql")]
        [InlineData("evolution1.sql")]
        [InlineData("evolution1.evo")]
        [InlineData("20181213145432evolution1.evo.sql")]
        public void Evolution_InvalidFileName(string fileName)
        {
            Assert.Throws<ArgumentException>(() => new Model.Evolution(fileName));
        }

        [Fact]
        public void Evolution_InvalidDate()
        {
            Assert.Throws<ArgumentNullException>(() => new Model.Evolution(null, "evolutionName"));
        }
    }
}
