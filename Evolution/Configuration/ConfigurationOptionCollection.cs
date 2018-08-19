using System.Collections.Generic;
using System.IO;

namespace Evolution.Configuration
{
    public class ConfigurationOptionCollection
    {
        private const string configFileName = ".config";
        private readonly List<ConfigurationOption> _configOptions;

        public ConfigurationOptionCollection()
        {
            var content = File.ReadAllLines($"./{configFileName}");

            foreach(var option in content)
            {
                if(!string.IsNullOrWhiteSpace(option))
                {
                    _configOptions.Add(new ConfigurationOption(option));
                }
            }
        }
    }
}