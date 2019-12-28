using System;
using Evolution.Configuration;
using Evolution.Exceptions;
using Xunit;

namespace Evolution.Test.Unit
{
    public class ConfigurationOptionTests
    {
        [Fact]
        [Trait("Category", "unit")]
        public void ParsesConfigurationOptionSucessfully_Key()
        {
            // Arrange
            const string key = "key";
            const string value = "value";
            string configLine = $"{key}={value}";

            // Act
            var result = new ConfigurationOption(configLine);

            // Assert
            Assert.Equal(result.Key, key);
        }

        [Fact]
        [Trait("Category", "unit")]
        public void ParsesConfigurationOptionSucessfully_Value()
        {
            // Arrange
            const string key = "key";
            const string value = "value";
            string configLine = $"{key}={value}";

            // Act
            var result = new ConfigurationOption(configLine);

            // Assert
            Assert.Equal(result.Value, value);
        }

        [Fact]
        [Trait("Category", "unit")]
        public void ParsesConfigurationOptionUnsucessfully_InvalidConfigFormat()
        {
            // Arrange
            const string key = "key";
            const string value = "value";
            string configLine = $"{key}{value}";

            // Act
            // Assert
            Assert.Throws<InvalidConfigurationException>(() => new ConfigurationOption(configLine));
        }
    }
}
