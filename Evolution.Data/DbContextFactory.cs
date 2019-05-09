using Evolution.Data;
using Evolution.Data.Oracle;
using Evolution.Data.SqlClient;
using Evolution.Model;
using System;

namespace Evolution.Data
{
    internal class DbContextFactory
    {
        public IEvolutionContext CreateContext(IDatabaseAuthenticationOptions dbAuthOptions)
        {
            switch (dbAuthOptions.Type)
            {
                case DatabaseTypes.Oracle:
                    {
                        var connectionStringBuilder = new OracleConnectionBuilder()
                        {
                            UserName = dbAuthOptions.User,
                            Password = dbAuthOptions.Password,
                            Server = dbAuthOptions.Server,
                            Instance = dbAuthOptions.Instance,
                            Port = dbAuthOptions.Port
                        };

                        return new OracleEvolutionContext(connectionStringBuilder.CreateConnectionString());
                    }

                case DatabaseTypes.MSSql:
                    {
                        var connectionStringBuilder = new SqlClientConnectionBuilder()
                        {
                            UserName = dbAuthOptions.User,
                            Password = dbAuthOptions.Password,
                            Server = dbAuthOptions.Server,
                            Instance = dbAuthOptions.Instance,
                            Port = dbAuthOptions.Port
                        };

                        return new SqlClientEvolutionContext(connectionStringBuilder.CreateConnectionString());
                    }

                default:
                    throw new NotSupportedException("The type of database provided is not supported");
            }
        }
    }
}
