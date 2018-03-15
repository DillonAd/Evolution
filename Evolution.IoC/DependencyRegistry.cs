using System;
using Evolution.Data;
using Evolution.Data.Oracle;
using Evolution.Repo;
using Microsoft.Extensions.DependencyInjection;

namespace Evolution.IoC
{
    public static class DependencyRegistry
    {
        public static IServiceProvider GetServiceProvider()
        {
            return new ServiceCollection()
                .AddTransient<IEvolutionRepo, EvolutionRepo>()
                .AddTransient<IEvolutionContext, OracleEvolutionContext>()
                .BuildServiceProvider();
        }
    }
}
