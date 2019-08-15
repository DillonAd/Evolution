using CommandLine;
using Evolution.Configuration;
using Evolution.Data;
using Evolution.Logic;
using Evolution.Model;
using Evolution.Options;
using Evolution.Repo;
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

        public static int Main(string[] args) =>
            Parser.Default.ParseArguments<AddOptions, ExecuteOptions>(args).
                MapResult(
                    (AddOptions opts) => Run(opts),
                    (ExecuteOptions opts) => Run(opts),
                    errors => 1
                );

        private static int Run(AddOptions options) =>
            GetApplication(options).Run(options);
        private static int Run(ExecuteOptions options) =>
            GetApplication(options).Run(options);
        
        private static IEvolutionLogic GetApplication(IDatabaseAuthenticationOptions dbAuthOptions)
        {
            PopulateMissingConfigValues(ref dbAuthOptions);
            var context = DbContextFactory.CreateContext(dbAuthOptions);
            var repo = new EvolutionRepo(context);
            var fileSystem = new FileContext();
            var fileSystemRepo = new FileRepo(fileSystem);
            return new EvolutionLogic(repo, fileSystemRepo);
        }

        private static void PopulateMissingConfigValues<TOption>(ref TOption options)
            where TOption : class
        {
            var newOptions = options;
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
