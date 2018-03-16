using Evolution.Data;
using Evolution.Repo;
using Microsoft.Extensions.DependencyInjection;

namespace Evolution
{
    public static class DependencyRegistry
    {
        public static IEvolutionRepo GetEvolutionRepo<T>() where T : class, IEvolutionContext
        {
            return new ServiceCollection()
                .AddTransient<IEvolutionContext, T>()
                .AddTransient<IEvolutionRepo, EvolutionRepo>()
                .BuildServiceProvider()
                .GetService<IEvolutionRepo>();
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
