using Evolution.Data;

namespace Evolution.Repo
{
    public interface IDbContextFactory
    {
        IEvolutionContext CreateContext<TContext>(IConnectionStringBuilder connectionStringBuilder) where TContext : IEvolutionContext;
    }
}
