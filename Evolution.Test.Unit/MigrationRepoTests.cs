using Evolution.Data;
using Evolution.Domain;
using Evolution.Repo;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Evolution.Test.Unit
{
    public class MigrationRepoTests
    {
        [Fact]
        public void GetExecutedMigrations()
        {
            var migrations = new List<IMigration>()
            {
                new Migration("Migration1"),
                new Migration("Migration2"),
                new Migration("Migration3")
            };

            var context = SetupMigrationContext(migrations);

            var repo = new MigrationRepo(context);
            var executedMigrations = repo.GetExecutedMigrations();

            Assert.Equal(migrations.Count, executedMigrations.Count());
        }

        private IMigrationContext SetupMigrationContext(List<IMigration> migrations)
        {
            var queryableMigrations = migrations.AsQueryable();
            
            var dbSetMock = new Mock<DbSet<IMigration>>();
            dbSetMock.As<IQueryable<IMigration>>().Setup(m => m.Provider).Returns(queryableMigrations.Provider);
            dbSetMock.As<IQueryable<IMigration>>().Setup(m => m.Expression).Returns(queryableMigrations.Expression);
            dbSetMock.As<IQueryable<IMigration>>().Setup(m => m.ElementType).Returns(queryableMigrations.ElementType);
            dbSetMock.As<IQueryable<IMigration>>().Setup(m => m.GetEnumerator()).Returns(() => queryableMigrations.GetEnumerator());
            dbSetMock.Setup(d => d.Add(It.IsAny<Migration>())).Callback<IMigration>((m) => migrations.Add(m));
            dbSetMock.Setup(d => d.Remove(It.IsAny<Migration>())).Callback<IMigration>((m) => migrations.Remove(m));

            var contextMock = new Mock<IMigrationContext>();
            contextMock.Setup(cm => cm.Migrations).Returns(dbSetMock.Object);

            return contextMock.Object;
        }
    }
}
