using Evolution.Model;
using NUnit.Framework;
using System;

namespace Evolution.Test.Unit
{
    public class EvolutionTests
    {
        [Test]
        [Category("unit")]
        [TestCase("initialize")]
        [TestCase("addTable")]
        [TestCase("add2Tables")]
        [TestCase("123456789")]
        [TestCase("a")]
        public void Evolution_ValidEvolutionName(string evolutionName)
        {
            var evolution = new Model.Evolution(evolutionName);
            Assert.AreEqual(evolutionName, evolution.Name);
        }

        [Test]
        [Category("unit")]
        [TestCase("")]
        [TestCase("   ")]
        public void Evolution_InvalidEvolutionName(string evolutionName)
        {
            Assert.Throws<ArgumentException>(() => new Model.Evolution(evolutionName));
        }

        [Test]
        [Category("unit")]
        public void Evolution_NullEvolutionName()
        {
            Assert.Throws<ArgumentNullException>(() => new Model.Evolution(null));
        }

        [Test]
        [Category("unit")]
        [TestCase("initialize")]
        [TestCase("addTable")]
        [TestCase("add2Tables")]
        [TestCase("123456789")]
        [TestCase("a")]
        public void Evolution_ValidFileNameFromEvolutionName(string evolutionName)
        {
            var evolution = new Model.Evolution(evolutionName);
            Assert.That(evolution.FileName, Does.Match(@"[0-9]{14}_\w{1,}.evo.sql"));
        }

        [Test]
        [Category("unit")]
        [TestCase("20181213145432_evolution1.evo.sql")]
        public void Evolution_ValidFileName(string fileName)
        {
            var evolution = new Model.Evolution(fileName);

            Assert.NotNull(evolution.Name);
            Assert.False(string.IsNullOrWhiteSpace(fileName));
        }

        [Test]
        [Category("unit")]
        [TestCase("evolution1.evo.sql")]
        [TestCase("evolution1.sql")]
        [TestCase("evolution1.evo")]
        [TestCase("20181213145432evolution1.evo.sql")]
        public void Evolution_InvalidFileName(string fileName)
        {
            Assert.Throws<ArgumentException>(() => new Model.Evolution(fileName));
        }
    }
}
