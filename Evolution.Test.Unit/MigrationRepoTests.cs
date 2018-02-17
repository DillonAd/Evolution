using Moq;
using System.Collections.Generic;
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
                new Migration("Migration1", string.Empty),
                new Migration("Migration2", string.Empty),
                new Migration("Migration3", string.Empty)
            };

            var contextMock = new Mock<IMigrationContext>();
            contextMock.SetupGet(m => m.Migrations).Returns(migrations);
            
            var repo = new MigrationRepo(contextMock.Object);
            var executedMigrations = repo.GetExecutedMigrations();

            Assert.Equal(migrations.Count, executedMigrations.Count);
        }
    }
}
