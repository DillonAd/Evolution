using Evolution.Domain;
using Microsoft.EntityFrameworkCore;

namespace Evolution.Data.Oracle
{
    public class OracleMigrationContext : IMigrationContext
    {
        DbSet<IMigration> IMigrationContext.Migrations { get; set; }
    }
}
