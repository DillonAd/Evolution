using Evolution.Data.Entity;
using System.Data.Entity;

namespace Evolution.Data.Oracle
{
    public class OracleEvolutionContext : DbContext, IEvolutionContext
    {
        public virtual DbSet<IEvolution> Evolutions { get; set; }

        public OracleEvolutionContext(string connectionString) : base(connectionString) { }

        public void ExecuteEvolution(string content)
        {
            Database.ExecuteSqlCommand(content);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IEvolution>();
        }
    }
}
