using CommandLine;
using Evolution.Data;
using Evolution.IoC;
using Evolution.Logic;
using Evolution.Model;
using Evolution.Options;
using System;

namespace Evolution
{
    class Program
    {
        public static int Main(string[] args)
        {               
            return Parser.Default.ParseArguments<AddOptions, ExecuteOptions>(args).
                MapResult(
                    (AddOptions opts) => Run(opts),
                    (ExecuteOptions opts) => Run(opts),
                    errors => 1
                );
        }

        public static int Run(AddOptions options)
        {
            var app = DependencyRegistry.GetApplication(options);
            return app.Run(options);
        }

        public static int Run(ExecuteOptions options)
        {
            var app = DependencyRegistry.GetApplication(options);
            return app.Run(options);
        }
    }
}
