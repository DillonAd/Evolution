using Evolution.Data;
using Evolution.Data.Oracle;
using Evolution.Model;
using Evolution.Repo;
using StructureMap;

namespace Evolution.IoC
{
    public static class DependencyRegistry
    {
        public static Container GetContainer<TContext, TConnectionStringBuilder>(IDatabaseAuthenticationOptions authOptions)
                    where TContext : class, IEvolutionContext
                    where TConnectionStringBuilder : class, IConnectionStringBuilder, new()
        {
            var container = new Container(_ =>
            {
                _.For<IDbContextFactory>().Use<DbContextFactory>();
                _.For<IFileContext>().Use<FileContext>();
                _.For<IFileRepo>().Use<FileRepo>();
                _.For<IEvolutionRepo>().Use<EvolutionRepo>();
                _.For<IConnectionStringBuilder>().Use(() => new TConnectionStringBuilder()
                {
                    UserName = authOptions.UserName,
                    Password = authOptions.Password,
                    Server = authOptions.Server,
                    Instance = authOptions.Instance
                });
            });

            var cb = container.GetInstance<IConnectionStringBuilder>();
            var factory = container.GetInstance<IDbContextFactory>();

            container.Configure(_ =>
            {
                _.For<IEvolutionContext>().Use<TContext>(() => (TContext)factory.CreateContext<TContext>(cb));
            });

            return container;
        }
    }
}
