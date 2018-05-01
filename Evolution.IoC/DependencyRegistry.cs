using Evolution.Data;
using Evolution.Logic;
using Evolution.Model;
using Evolution.Repo;
using Microsoft.Extensions.DependencyInjection;

namespace Evolution.IoC
{
    public static class DependencyRegistry
    {
        public static IEvolutionLogic GetApplication(IDatabaseAuthenticationOptions dbAuthOptions)
        {
            return new ServiceCollection()
            .AddTransient((ctx) => new DbContextFactory().CreateContext(dbAuthOptions))
            .AddTransient<IEvolutionRepo, EvolutionRepo>()
            .AddTransient<IFileContext, FileContext>()
            .AddTransient<IFileRepo, FileRepo>()
            .AddTransient<IEvolutionLogic, EvolutionLogic>()
            .BuildServiceProvider()
            .GetService<IEvolutionLogic>();
        }
    }
}
