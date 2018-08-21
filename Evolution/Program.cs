using CommandLine;
using Evolution.Configuration;
using Evolution.IoC;
using Evolution.Options;
using System;

namespace Evolution
{
    public static class Program
    {
        private static ConfigurationOptionCollection _configOptions { get; }

        static Program()
        {
            _configOptions = new ConfigurationOptionCollection();
        }

        public static int Main(string[] args)
        {
            return Parser.Default.ParseArguments<AddOptions, ExecuteOptions>(args).
                MapResult(
                    (AddOptions opts) => Run(opts),
                    (ExecuteOptions opts) => Run(opts),
                    errors => 1
                );
        }

        private static int Run(AddOptions options)
        {
            PopulateMissingConfigValues(ref options);
            var app = DependencyRegistry.GetApplication(options);
            return app.Run(options);
        }

        private static int Run(ExecuteOptions options)
        {
            PopulateMissingConfigValues(ref options);
            var app = DependencyRegistry.GetApplication(options);
            return app.Run(options);
        }

        private static void PopulateMissingConfigValues<TOption>(ref TOption options)
            where TOption : class
        {
            string configValue;
            object parseValue;

            foreach(var property in options.GetType().GetProperties())
            {
                configValue = _configOptions.GetConfigurationValue(property.Name);

                if(string.IsNullOrWhiteSpace(configValue))
                {
                    continue;
                }

                //Don't override values that are passed in via CLI
                if(property.GetValue(options)?.ToString() == configValue)
                {
                    continue;
                }

                if(property.PropertyType.IsEnum)
                {
                    parseValue = Enum.Parse(property.PropertyType, configValue) as Enum;
                }
                else
                {
                    parseValue = Convert.ChangeType(configValue, property.PropertyType);
                }

                property.SetValue(options, parseValue);
            }
        }
    }
}
