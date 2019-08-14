using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Evolution.Data.Entity;

namespace Evolution.Data.SqlClient 
{
    public sealed class SqlClientEvolutionContext : IEvolutionContext 
    {
        private readonly SqlConnection _Connection;

        public SqlClientEvolutionContext (string connectionString) 
        {
            _Connection = new SqlConnection(connectionString);
            _Connection.Open();

            CreateEvolutionTable();
        }

        public void AddEvolution(IEvolution evolution) 
        {
            const string insertCommand = @"INSERT INTO EVOLUTION (ID, NAME, FILE_NAME, CONTENT, [CHECKPOINT])
                                VALUES (@ID, @NAME, @FILE_NAME, @CONTENT, @CHECKPOINT)";

            using (SqlCommand cmd = new SqlCommand(insertCommand, _Connection)) 
            {
                cmd.Parameters.Add ("ID", SqlDbType.UniqueIdentifier).Value = evolution.Id;
                cmd.Parameters.Add ("NAME", SqlDbType.VarChar, 100).Value = evolution.Name;
                cmd.Parameters.Add ("FILE_NAME", SqlDbType.VarChar, 100).Value = evolution.FileName;
                cmd.Parameters.Add ("CONTENT", SqlDbType.VarChar, -1).Value = evolution.Content;
                cmd.Parameters.Add ("CHECKPOINT", SqlDbType.TinyInt).Value = evolution.CheckPoint;
                cmd.ExecuteNonQuery();
            }
        }

        public void ExecuteEvolution(string content) 
        {
            foreach (var command in content.Split(new string[] { "GO" }, StringSplitOptions.RemoveEmptyEntries)) 
            {
                Console.WriteLine ($"- {command}");
                
                using (SqlCommand cmd = new SqlCommand(command, _Connection)) 
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
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
                                                   [CHECKPOINT] 
                                            FROM EVOLUTION";

            var results = new DataTable();
            List<IEvolution> evolutions = new List<IEvolution>();
            IEvolution evolution;

            using (SqlCommand cmd = new SqlCommand(evolutionQuery, _Connection)) 
            {
                using (SqlDataReader r = cmd.ExecuteReader()) 
                {
                    results.Load (r);
                }
            }

            foreach (DataRow row in results.Rows) 
            {
                evolution = new Entity.Evolution() 
                {
                    Id = (Guid)row["ID"],
                    Name = row["NAME"].ToString(),
                    FileName = row["FILE_NAME"].ToString(),
                    Content = row["CONTENT"].ToString(),
                };

                if(!int.TryParse(row["CHECKPOINT"].ToString(), out var checkpoint))
                {
                    checkpoint = 0;
                }

                evolution.CheckPoint = checkpoint;

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
                                    ID              UNIQUEIDENTIFIER,
                                    NAME            VARCHAR(100),
                                    FILE_NAME       VARCHAR(100),
                                    CREATED_DATE    DATETIME,
                                    CONTENT         VARCHAR(MAX),
                                    [CHECKPOINT]    BIT,

                                    PRIMARY KEY (ID)
                                )";

            const string checkTableQuery = @"SELECT COUNT(*) AS CNT
                                            FROM SYS.TABLES
                                            WHERE NAME = 'EVOLUTION'";

            using (var cmd = new SqlCommand(checkTableQuery, _Connection)) 
            {
                using (var r = cmd.ExecuteReader()) 
                {
                    var result = new DataTable();
                    result.Load (r);

                    if (Convert.ToInt32(result.Rows[0]["CNT"]) > 0) 
                    {
                        return;
                    }
                }
            }

            using (var cmd = new SqlCommand(createCommand, _Connection)) 
            {
                cmd.ExecuteNonQuery();
            }
        }
    }
}