using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Evolution.Configuration
{
    public class ConfigurationOptionCollection
    {
        private const string configFileName = ".evo";
        private readonly List<ConfigurationOption> _configOptions;

        public ConfigurationOptionCollection()
        {
            _configOptions = new List<ConfigurationOption>();
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), configFileName);
            var content = File.ReadAllLines($"{filePath}");
            
            foreach(var option in content)
            {
                if(!string.IsNullOrWhiteSpace(option))
                {
                    _configOptions.Add(new ConfigurationOption(option));
                }
            }
        }

        public string GetConfigurationValue(string key) =>
            _configOptions.FirstOrDefault(co => 
                co.Key.Trim().ToLower() == key.Trim().ToLower())?.Value;
    }
}