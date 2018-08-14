using Evolution.Data.Entity;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;

namespace Evolution.Data.Oracle
{
    public sealed class OracleEvolutionContext : IEvolutionContext
    {
        private readonly OracleConnection _Connection;

        public OracleEvolutionContext(string connectionString)
        {
            _Connection = new OracleConnection(connectionString);
            _Connection.Open();

            CreateEvolutionTable();
        }

        public void AddEvolution(IEvolution evolution)
        {
            const string insertCommand = @"INSERT INTO EVOLUTION (ID, NAME, FILE_NAME, CONTENT, HASH, CHECKPOINT)
                                VALUES (@ID, @NAME, @FILE_NAME, @CONTENT, @HASH, @CHECKPOINT)";

            using (OracleCommand cmd = new OracleCommand(insertCommand, _Connection))
            {
                cmd.Parameters.Add("@ID", evolution.Id);
                cmd.Parameters.Add("@NAME", evolution.Name);
                cmd.Parameters.Add("@FILE_NAME", evolution.FileName);
                cmd.Parameters.Add("@CONTENT", evolution.Content);
                cmd.Parameters.Add("@HASH", evolution.Hash);
                cmd.Parameters.Add("@CHECKPOINT", evolution.CheckPoint);
                cmd.ExecuteNonQuery();
            }
        }

        public void ExecuteEvolution(string content)
        {
            foreach (var command in content.Split('/'))
            {
                if (!string.IsNullOrWhiteSpace(command))
                {
                    Console.WriteLine(command);
                    using (OracleCommand cmd = new OracleCommand(command, _Connection))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public List<IEvolution> GetEvolutions()
        {
            const string evolutionQuery = @"SELECT ID, 
                                                   NAME, 
                                                   FILE_NAME, 
                                                   CREATED_DATE, 
                                                   CONTENT, 
                                                   HASH, 
                                                   CHECKPOINT 
                                            FROM EVOLUTION";

            var results = new DataTable();
            List<IEvolution> evolutions = new List<IEvolution>();
            IEvolution evolution;

            using (OracleCommand cmd = new OracleCommand(evolutionQuery, _Connection))
            {
                using (OracleDataReader r = cmd.ExecuteReader())
                {
                    results.Load(r);
                }
            }

            foreach (DataRow row in results.Rows)
            {
                evolution = new Entity.Evolution()
                {
                    Id = Guid.Parse(row["ID"].ToString()),
                    Name = row["NAME"].ToString(),
                    FileName = row["FILE_NAME"].ToString(),
                    Content = row["CONTENT"].ToString(),
                    //TODO Implement hash
                    //Hash = row["HASH"], Convert.to
                    CheckPoint = int.Parse(row["CHECKPOINT"].ToString())
                };

                evolutions.Add(evolution);
            }

            return evolutions;
        }

        public void Dispose()
        {
            if (_Connection.State == ConnectionState.Open)
            {
                _Connection.Close();
            }

            _Connection.Dispose();
        }

        private void CreateEvolutionTable()
        {
            const string createCommand = @"CREATE TABLE EVOLUTION
                                (
                                    ID              RAW(16),
                                    NAME            VARCHAR2(100),
                                    FILE_NAME       VARCHAR2(100),
                                    CREATED_DATE    DATE,
                                    CONTENT         CLOB,
                                    HASH            RAW(16),
                                    CHECKPOINT      NUMBER(1),

                                    CONSTRAINT EVOLUTION_PK PRIMARY KEY (ID)
                                )";

            const string checkTableQuery = @"SELECT COUNT(*) AS CNT
                                            FROM DBA_TABLES
                                            WHERE TABLE_NAME = 'EVOLUTION'";

            using (var cmd = new OracleCommand(checkTableQuery, _Connection))
            {
                using (var r = cmd.ExecuteReader())
                {
                    var result = new DataTable();
                    result.Load(r);

                    if (Convert.ToInt32(result.Rows[0]["CNT"]) > 0)
                    {
                        return;
                    }
                }
            }

            using (var cmd = new OracleCommand(createCommand, _Connection))
            {
                cmd.ExecuteNonQuery();
            }
        }
    }
}
