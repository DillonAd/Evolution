using Evolution.Data;
using Evolution.Data.Entity;
using System.Data.Entity;
using System;
using System.Data.Common;

namespace Evolution.Data.Oracle
{
    public class OracleEvolutionContext : DbContext, IEvolutionContext
    {
        public virtual DbSet<IProgression> Evolutions { get; set; }

        public OracleEvolutionContext(DbConnection connection) : base(connection.ConnectionString) { }
        
        public void ExecuteEvolution(string content)
        {
            Database.ExecuteSqlCommand(content);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IProgression>();
        }
    }
}
