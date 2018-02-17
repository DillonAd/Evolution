using Evolution.Data;
using Evolution.Domain;
using Evolution.Exceptions;
using System;
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

        public void AddMigration(IMigration migration)
        {
            _Context.Migrations.Add(migration);
        }

        public void RemoveMigration(IMigration migration)
        {
            _Context.Migrations.Remove(migration);
        }

        public void SaveChanges()
        {
            _Context.SaveChanges();
        }

        public void ExecuteMigration(string content)
        {
            _Context.ExecuteMigration(content);
        }
    }
}
