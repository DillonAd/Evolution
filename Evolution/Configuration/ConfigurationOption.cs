using Evolution.Exceptions;

namespace Evolution.Configuration
{
    public class ConfigurationOption
    {
        public string Key { get; }
        public string Value { get; }

        public ConfigurationOption(string configurationFileLine)
        {
            var equalityIndex = configurationFileLine.IndexOf('=');

            if(equalityIndex < 0)
            {
                throw new InvalidConfigurationException(
                    $"Configuration option {configurationFileLine} is in an invalid format. \n" +
                    "Valid Format: <key>=<value>");
            }

            Key = configurationFileLine.Substring(0, equalityIndex);
            Value = configurationFileLine.Substring(equalityIndex + 1);
        }
    }
}