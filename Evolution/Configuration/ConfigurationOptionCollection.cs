using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Evolution.Configuration
{
    public class ConfigurationOptionCollection
    {
        private const string configFileName = ".config";
        private readonly List<ConfigurationOption> _configOptions;

        public ConfigurationOptionCollection()
        {
            _configOptions = new List<ConfigurationOption>();
            
            var content = File.ReadAllLines($"./{configFileName}");

            foreach(var option in content)
            {
                if(!string.IsNullOrWhiteSpace(option))
                {
                    _configOptions.Add(new ConfigurationOption(option));
                }
            }
        }

        public string GetConfigurationValue(string key) =>
            _configOptions.FirstOrDefault(co => co.Key == key)?.Value;
    }
}