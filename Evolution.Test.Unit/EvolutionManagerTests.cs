using Evolution.Domain;
using Evolution.Repo;
using Moq;
using System.Linq;
using Xunit;

namespace Evolution.Test.Unit
{
    public class EvolutionManagerTests
    {
        [Fact]
        public void Evolve_Success()
        {
            var executedMigrations = new Migration[] { };

            var migrationFileNames = new string[]
            {
                "Migration1.up.sql",
                "Migration1.down.sql",
                "Migration2.up.sql",
                "Migration2.down.sql",
                "Migration3.up.sql",
                "Migration3.down.sql",
                "Migration4.up.sql",
                "Migration4.down.sql",
                "Migration5.up.sql",
                "Migration5.down.sql",
                "Migration6.up.sql",
                "Migration6.down.sql"
            };

            var mockMigrationRepo = new Mock<IMigrationRepo>();
            mockMigrationRepo.Setup(r => r.GetExecutedMigrations()).Returns(executedMigrations.AsEnumerable());
            mockMigrationRepo.Setup(r => r.ExecuteMigration(It.IsAny<string>()));

            var mockFileRepo = new Mock<IFileRepo>();
            mockFileRepo.Setup(r => r.GetUnexecutedMigrations(It.Is<Migration[]>(x => x == executedMigrations)))
                .Returns(migrationFileNames);

            var manager = new EvolutionManager(mockMigrationRepo.Object, mockFileRepo.Object);
            manager.Evolve();

            mockMigrationRepo.Verify(r => r.ExecuteMigration(It.IsAny<string>()), Times.Exactly(migrationFileNames.Length / 2));
        }
    }
}
