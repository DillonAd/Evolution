using Evolution.Data;
using Evolution.Data.Oracle;
using Evolution.Data.SqlClient;
using Evolution.Model;
using System;

namespace Evolution
{
    internal static class DbContextFactory
    {
        public static IEvolutionContext CreateContext(IDatabaseAuthenticationOptions dbAuthOptions)
        {
            IConnectionStringBuilder connectionStringBuilder;
            IEvolutionContext context;

            switch (dbAuthOptions.Type)
            {
                case DatabaseTypes.Oracle:
                    connectionStringBuilder = new OracleConnectionBuilder()
                    {
                        UserName = dbAuthOptions.User,
                        Password = dbAuthOptions.Password,
                        Server = dbAuthOptions.Server,
                        Instance = dbAuthOptions.Instance,
                        Port = dbAuthOptions.Port
                    };

                    context = new OracleEvolutionContext(connectionStringBuilder.CreateConnectionString());
                    break;

                case DatabaseTypes.MSSql:
                    connectionStringBuilder = new SqlClientConnectionBuilder()
                    {
                        UserName = dbAuthOptions.User,
                        Password = dbAuthOptions.Password,
                        Server = dbAuthOptions.Server,
                        Instance = dbAuthOptions.Instance,
                        Port = dbAuthOptions.Port
                    };

                    context = new SqlClientEvolutionContext(connectionStringBuilder.CreateConnectionString());
                    break;

                default:
                    throw new NotSupportedException("The type of database provided is not supported");
            }

            return context;
        }
    }
}