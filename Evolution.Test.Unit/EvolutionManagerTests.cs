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
            var executedEvolutions = new IProgression[] { };

            var evolutionsToExecute = new IProgression[]
            {
                new Progression() { Name = "Evolution1", FileName = "Evolution1.evo.sql" },
                new Progression() { Name = "Evolution2", FileName = "Evolution2.evo.sql" },
                new Progression() { Name = "Evolution3", FileName = "Evolution3.evo.sql" },
                new Progression() { Name = "Evolution4", FileName = "Evolution4.evo.sql" },
                new Progression() { Name = "Evolution5", FileName = "Evolution5.evo.sql" },
                new Progression() { Name = "Evolution6", FileName = "Evolution6.evo.sql" },
            };

            var mockEvolutionRepo = new Mock<IEvolutionRepo>();
            mockEvolutionRepo.Setup(r => r.GetExecutedEvolutions()).Returns(executedEvolutions);
            mockEvolutionRepo.Setup(r => r.ExecuteEvolution(It.IsAny<string>()));

            var mockFileRepo = new Mock<IFileRepo>();
            mockFileRepo.Setup(r => r.GetUnexecutedEvolutions(It.Is<IProgression[]>(x => x == executedEvolutions)))
                .Returns(evolutionsToExecute);

            var manager = new EvolutionManager(mockEvolutionRepo.Object, mockFileRepo.Object);
            manager.Evolve();

            mockFileRepo.Verify(r => r.GetEvolutionFileContent(It.IsAny<string>()), Times.Exactly(evolutionsToExecute.Length));
            mockEvolutionRepo.Verify(r => r.ExecuteEvolution(It.IsAny<string>()), Times.Exactly(evolutionsToExecute.Length));
            mockEvolutionRepo.Verify(r => r.AddEvolution(It.IsAny<IProgression>()), Times.Exactly(evolutionsToExecute.Length));
        }
    }
}
