using Evolution.Data;
using Evolution.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Evolution.Repo
{
    public class MigrationRepo : IMigrationRepo
    {
        private readonly IMigrationContext _Context;

        public MigrationRepo(IMigrationContext context)
        {
            _Context = context;
        }

        public IEnumerable<IMigration> GetExecutedMigrations()
        {
            return _Context.Migrations.AsEnumerable();
        }
    }
}
