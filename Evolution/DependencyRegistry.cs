using Evolution.Data;
using Evolution.Data.Oracle;
using Evolution.Options;
using Evolution.Repo;
using Microsoft.Extensions.DependencyInjection;

namespace Evolution
{
    public static class DependencyRegistry
    {
        public static IEvolutionRepo GetEvolutionRepo<TContext, TConnectionStringBuilder>(IDatabaseAuthenticationOptions authOptions)
                where TContext : class, IEvolutionContext
                where TConnectionStringBuilder : class, IConnectionStringBuilder, new()
        {
            return new ServiceCollection()
                .AddTransient<IDbContextFactory, DbContextFactory>()
                .AddTransient<IConnectionStringBuilder>((ctx) => new TConnectionStringBuilder()
                {
                    UserName = authOptions.UserName,
                    Password = authOptions.Password,
                    Server = authOptions.Server,
                    Instance = authOptions.Instance
                })
                .AddTransient<IEvolutionContext, TContext>((ctx) =>
                {
                    var cb = ctx.GetService<IConnectionStringBuilder>();
                    var factory = ctx.GetService<IDbContextFactory>();
                    return (TContext)factory.CreateContext<TContext>(cb);
                })
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
