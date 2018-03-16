using Evolution.Data;
using Evolution.Data.Oracle;
using System;

namespace Evolution.Repo
{
    public class DbContextFactory : IDbContextFactory
    {
        public IEvolutionContext CreateContext<TContext>(IConnectionStringBuilder connectionStringBuilder) 
            where TContext : IEvolutionContext
        {
            if(typeof(TContext) == typeof(OracleEvolutionContext))
            {
                return new OracleEvolutionContext(connectionStringBuilder.CreateConnectionString());
            }
            else
            {
                throw new NotSupportedException("The DB context provided is not supported : " + typeof(TContext).AssemblyQualifiedName);
            }
        }
    }
}
