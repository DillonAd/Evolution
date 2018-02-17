using Evolution.Domain;
using Microsoft.EntityFrameworkCore;

namespace Evolution.Data
{
    public interface IMigrationContext
    {
        DbSet<IMigration> Migrations { get; set; }
    }
}
