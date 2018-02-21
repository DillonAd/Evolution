using Evolution.Domain;
using Evolution.Model;
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
            var executedMigrations = new IProgression[] { };

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

            var mockMigrationRepo = new Mock<IEvolutionRepo>();
            mockMigrationRepo.Setup(r => r.GetExecutedEvolutions()).Returns(executedMigrations);
            mockMigrationRepo.Setup(r => r.ExecuteEvolution(It.IsAny<string>()));

            var mockFileRepo = new Mock<IFileRepo>();
            //mockFileRepo.Setup(r => r.GetUnexecutedEvolutions(It.Is<Model.Evolution[]>(x => x == executedMigrations)))
            //    .Returns(migrationFileNames);

            var manager = new EvolutionManager(mockMigrationRepo.Object, mockFileRepo.Object);
            manager.Evolve();

            mockMigrationRepo.Verify(r => r.ExecuteEvolution(It.IsAny<string>()), Times.Exactly(migrationFileNames.Length / 2));
        }
    }
}
