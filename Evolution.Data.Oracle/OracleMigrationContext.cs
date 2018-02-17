using Evolution.Domain;
using System.Data.Entity;
using System;
using System.Data.Common;

namespace Evolution.Data.Oracle
{
    public class OracleMigrationContext : DbContext, IMigrationContext
    {
        public virtual DbSet<IMigration> Migrations { get; set; }

        public OracleMigrationContext(DbConnection connection) : base(connection.ConnectionString) { }
        
        public void ExecuteMigration(string content)
        {
            Database.ExecuteSqlCommand(content);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Migration>();
        }
    }
}
