using Evolution.Data;
using Evolution.Data.Oracle;
using Evolution.Model;
using System;

namespace Evolution.IoC
{
    internal class DbContextFactory
    {
        public IEvolutionContext CreateContext(IDatabaseAuthenticationOptions dbAuthOptions)
        {
            if(dbAuthOptions.DatabaseType == DatabaseTypes.Oracle)
            {
                var connectionStringBuilder = new OracleConnectionBuilder()
                {
                    UserName = dbAuthOptions.UserName,
                    Password = dbAuthOptions.Password,
                    Server = dbAuthOptions.Server,
                    Instance = dbAuthOptions.Instance,
                    Port = dbAuthOptions.Port
                };

                return new OracleEvolutionContext(connectionStringBuilder.CreateConnectionString());
            }
            else
            {
                throw new NotSupportedException("The type of database provided is not supported");
            }
        }
    }
}
