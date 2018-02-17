using Evolution.Domain;
using Microsoft.EntityFrameworkCore;

namespace Evolution.Data.Oracle
{
    public class OracleMigrationContext : DbContext, IMigrationContext
    {
        DbSet<IMigration> IMigrationContext.Migrations { get; set; }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
