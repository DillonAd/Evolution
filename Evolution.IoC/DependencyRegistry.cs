using System;
using Evolution.Data;
using Evolution.Data.Oracle;
using Evolution.Repo;
using Microsoft.Extensions.DependencyInjection;

namespace Evolution.IoC
{
    public static class DependencyRegistry
    {
        public static IServiceProvider GetDataServiceProvider<T>() where T : class, IEvolutionContext
        {
            return new ServiceCollection()
                .AddTransient<IEvolutionContext, T>()
                .AddTransient<IEvolutionRepo, EvolutionRepo>()
                .BuildServiceProvider();
        }

        public static IFileRepo GetFileRepo()
        {
            return new ServiceCollection()
                .AddTransient<IFileContext, FileContext>()
                .AddTransient<IFileRepo, FileRepo>()
                .BuildServiceProvider()
                .GetService<IFileRepo>();
        }
    }
}
