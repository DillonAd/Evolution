using Evolution.Data.Entity;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;

namespace Evolution.Data.Oracle
{
    public class OracleEvolutionContext : IEvolutionContext, IDisposable
    {
        private readonly OracleConnection _Connection;

        public OracleEvolutionContext(string connectionString)
        {
            _Connection = new OracleConnection(connectionString);
            _Connection.Open();

            CreateEvolutionTable();
        }

        public void ExecuteEvolution(string content)
        {
            using (OracleCommand cmd = new OracleCommand(content, _Connection))
            {
                cmd.ExecuteNonQuery();
            }
        }

        public void Dispose()
        {
            if(_Connection.State == ConnectionState.Open)
            {
                _Connection.Close();
            }

            _Connection.Dispose();
        }

        private void CreateEvolutionTable()
        {
            var createCommand = @"CREATE TABLE EVOLUTION
                                (
                                    
                                )";
        }
    }
}
