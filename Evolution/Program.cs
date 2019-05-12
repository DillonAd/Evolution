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
            var context = DbContextFactory.CreateContext(dbAuthOptions);
            var repo = new EvolutionRepo(context);
            var fileSystem = new FileContext();
            var fileSystemRepo = new FileRepo(fileSystem);
            return new EvolutionLogic(repo, fileSystemRepo);
        }
    }
}
