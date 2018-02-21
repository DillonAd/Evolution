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

            var evolutionFileNames = new string[]
            {
                "Evolution1.up.sql",
                "Evolution2.up.sql",
                "Evolution3.up.sql",
                "Evolution4.up.sql",
                "Evolution5.up.sql",
                "Evolution6.up.sql"
            };

            var mockEvolutionRepo = new Mock<IEvolutionRepo>();
            mockEvolutionRepo.Setup(r => r.GetExecutedEvolutions()).Returns(executedEvolutions);
            mockEvolutionRepo.Setup(r => r.ExecuteEvolution(It.IsAny<string>()));

            var mockFileRepo = new Mock<IFileRepo>();
            //mockFileRepo.Setup(r => r.GetUnexecutedEvolutions(It.Is<Model.Evolution[]>(x => x == executedEvolutions)))
            //    .Returns(evolutionFileNames);

            var manager = new EvolutionManager(mockEvolutionRepo.Object, mockFileRepo.Object);
            manager.Evolve();

            mockEvolutionRepo.Verify(r => r.ExecuteEvolution(It.IsAny<string>()), Times.Exactly(evolutionFileNames.Length / 2));
        }
    }
}
