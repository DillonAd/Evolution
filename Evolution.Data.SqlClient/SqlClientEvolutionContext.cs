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
            _Connection = new SqlConnection (connectionString);
            _Connection.Open ();

            CreateEvolutionTable ();
        }

        public void AddEvolution (IEvolution evolution) 
        {
            const string insertCommand = @"INSERT INTO EVOLUTION (ID, NAME, FILE_NAME, CONTENT, HASH, CHECKPOINT)
                                VALUES (:ID, :NAME, :FILE_NAME, :CONTENT, :HASH, :CHECKPOINT)";

            using (SqlCommand cmd = new SqlCommand (insertCommand, _Connection)) 
            {
                cmd.Parameters.Add ("ID", SqlDbType.VarBinary, 16).Value = evolution.Id.ToByteArray ();
                cmd.Parameters.Add ("NAME", SqlDbType.VarChar, 100).Value = evolution.Name;
                cmd.Parameters.Add ("FILE_NAME", SqlDbType.VarChar, 100).Value = evolution.FileName;
                cmd.Parameters.Add ("CONTENT", SqlDbType.VarChar, -1).Value = evolution.Content;
                cmd.Parameters.Add ("HASH", SqlDbType.VarBinary, 16).Value = evolution.Hash;
                cmd.Parameters.Add ("CHECKPOINT", SqlDbType.TinyInt).Value = evolution.CheckPoint;
                cmd.ExecuteNonQuery ();
            }
        }

        public void ExecuteEvolution (string content) 
        {
            foreach (var command in content.Split (new char[] { '/' })) 
            {
                if (!string.IsNullOrWhiteSpace (command)) {
                    Console.WriteLine ($"- {command}");
                    using (SqlCommand cmd = new SqlCommand (command, _Connection)) 
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery ();
                    }
                }
            }
        }

        public List<IEvolution> GetEvolutions () 
        {
            const string evolutionQuery = @"SELECT ID, 
                                                   NAME, 
                                                   FILE_NAME, 
                                                   CREATED_DATE, 
                                                   CONTENT, 
                                                   HASH, 
                                                   CHECKPOINT 
                                            FROM EVOLUTION";

            var results = new DataTable ();
            List<IEvolution> evolutions = new List<IEvolution> ();
            IEvolution evolution;

            using (SqlCommand cmd = new SqlCommand (evolutionQuery, _Connection)) 
            {
                using (SqlDataReader r = cmd.ExecuteReader ()) 
                {
                    results.Load (r);
                }
            }

            foreach (DataRow row in results.Rows) 
            {
                evolution = new Entity.Evolution () 
                {
                    Id = new Guid ((byte[]) row["ID"]),
                    Name = row["NAME"].ToString (),
                    FileName = row["FILE_NAME"].ToString (),
                    Content = row["CONTENT"].ToString (),
                    //TODO Implement hash
                    //Hash = row["HASH"], Convert.to
                    CheckPoint = int.Parse (row["CHECKPOINT"].ToString ())
                };

                evolutions.Add(evolution);
            }

            return evolutions;
        }

        public void Dispose () 
        {
            if (_Connection.State == ConnectionState.Open) 
            {
                _Connection.Close ();
            }

            _Connection.Dispose ();
        }

        private void CreateEvolutionTable () 
        {
            const string createCommand = @"CREATE TABLE EVOLUTION
                                (
                                    ID              VARBINARY(16),
                                    NAME            VARCHAR(100),
                                    FILE_NAME       VARCHAR(100),
                                    CREATED_DATE    DATE,
                                    CONTENT         VARCHAR(MAX),
                                    HASH            VARBINARY(16),
                                    CHECKPOINT      TINYINT,

                                    CONSTRAINT EVOLUTION_PK PRIMARY KEY (ID)
                                )";

            const string checkTableQuery = @"SELECT COUNT(*) AS CNT
                                            FROM DBA_TABLES
                                            WHERE TABLE_NAME = 'EVOLUTION'";

            using (var cmd = new SqlCommand (checkTableQuery, _Connection)) 
            {
                using (var r = cmd.ExecuteReader ()) 
                {
                    var result = new DataTable ();
                    result.Load (r);

                    if (Convert.ToInt32 (result.Rows[0]["CNT"]) > 0) 
                    {
                        return;
                    }
                }
            }

            using (var cmd = new SqlCommand (createCommand, _Connection)) 
            {
                cmd.ExecuteNonQuery ();
            }
        }
    }
}