using System;
using Evolution.Logic;
using Evolution.Repo;
using Evolution.Test.Unit.UnitTests.Infrastructure.Options;
using Moq;
using Xunit;

namespace Evolution.Test.Unit.UnitTests
{
    public class EvolutionLogicTests
    {
        private readonly Mock<IEvolutionRepo> _evolutionRepo;
        private readonly Mock<IFileRepo> _fileRepo;

        public EvolutionLogicTests()
        {
            _evolutionRepo = new Mock<IEvolutionRepo>();
            _fileRepo = new Mock<IFileRepo>();
        }

        [Fact]
        [Trait("Category", "unit")]
        public void RunEvolutionCreatable_WhenEvolutionFileCreatedSuccessfully()
        {
            // Arrange
            _fileRepo.Setup(s => s.CreateEvolutionFile(It.IsAny<Model.Evolution>(), It.IsAny<string>())).Verifiable();
            var newEvolution = new MockCreatable();
            const int expected = 0;

            // Act
            var actual = new EvolutionLogic(_evolutionRepo.Object, _fileRepo.Object).Run(newEvolution);

            // Assert
            Assert.Equal(expected, actual);
            _fileRepo.Verify(v => v.CreateEvolutionFile(It.IsAny<Model.Evolution>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        [Trait("Category", "unit")]
        public void RunEvolutionCreatable_WhenEvolutionFileCreationThrowsException()
        {
            // Arrange
            _fileRepo.Setup(s => s.CreateEvolutionFile(It.IsAny<Model.Evolution>(), It.IsAny<string>())).Throws(new Exception());
            var newEvolution = new MockCreatable();
            const int expected = 1;

            // Act
            var actual = new EvolutionLogic(_evolutionRepo.Object, _fileRepo.Object).Run(newEvolution);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        [Trait("Category", "unit")]
        public void RunEvolutionTargetable_WhenUnexecutedEvolutionsExist_ThenEvolutionFilesCreated()
        {
            // Arrange
            _evolutionRepo.Setup(s => s.GetExecutedEvolutionFileNames()).Returns(new[] {"date_ExFile1", "date_ExFile2"});
            _evolutionRepo.Setup(s => s.ExecuteEvolution(It.IsAny<string>())).Verifiable();
            _evolutionRepo.Setup(s => s.AddEvolution(It.IsAny<Model.Evolution>(), It.IsAny<string>())).Verifiable();
            _fileRepo.Setup(s => s.GetUnexecutedEvolutionFiles(It.IsAny<string[]>())).Returns(new[] {"date_UnExFile1"});
            _fileRepo.Setup(s => s.GetEvolutionFileContent(It.IsAny<Model.Evolution>())).Returns(string.Empty);
            var newEvolution = new MockTargetable();
            const int expected = 0;

            // Act
            var actual = new EvolutionLogic(_evolutionRepo.Object, _fileRepo.Object).Run(newEvolution);

            // Assert
            Assert.Equal(expected, actual);
            _evolutionRepo.Verify(v => v.ExecuteEvolution(It.IsAny<string>()), Times.Once);
            _evolutionRepo.Verify(v => v.AddEvolution(It.IsAny<Model.Evolution>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        [Trait("Category", "unit")]
        public void RunEvolutionTargetable_WhenNoUnexecutedEvolutionsExist_ThenEvolutionFilesCreated()
        {
            // Arrange
            _evolutionRepo.Setup(s => s.GetExecutedEvolutionFileNames()).Returns(new[] { "date_ExFile1", "date_ExFile2" });
            _evolutionRepo.Setup(s => s.ExecuteEvolution(It.IsAny<string>())).Verifiable();
            _evolutionRepo.Setup(s => s.AddEvolution(It.IsAny<Model.Evolution>(), It.IsAny<string>())).Verifiable();
            _fileRepo.Setup(s => s.GetUnexecutedEvolutionFiles(It.IsAny<string[]>())).Returns(new string[] {});
            _fileRepo.Setup(s => s.GetEvolutionFileContent(It.IsAny<Model.Evolution>())).Returns(string.Empty);
            var newEvolution = new MockTargetable();
            const int expected = 0;

            // Act
            var actual = new EvolutionLogic(_evolutionRepo.Object, _fileRepo.Object).Run(newEvolution);

            // Assert
            Assert.Equal(expected, actual);
            _evolutionRepo.Verify(v => v.ExecuteEvolution(It.IsAny<string>()), Times.Never);
            _evolutionRepo.Verify(v => v.AddEvolution(It.IsAny<Model.Evolution>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        [Trait("Category", "unit")]
        public void RunEvolutionTargetable_WhenExceptionThrown_ThenResultIsCorrect()
        {
            // Arrange
            _evolutionRepo.Setup(s => s.GetExecutedEvolutionFileNames()).Returns(new string[] { });
            _evolutionRepo.Setup(s => s.ExecuteEvolution(It.IsAny<string>())).Verifiable();
            _evolutionRepo.Setup(s => s.AddEvolution(It.IsAny<Model.Evolution>(), It.IsAny<string>())).Verifiable();
            _fileRepo.Setup(s => s.GetUnexecutedEvolutionFiles(It.IsAny<string[]>())).Returns(new[] { "UnExFile1" });
            _fileRepo.Setup(s => s.GetEvolutionFileContent(It.IsAny<Model.Evolution>())).Returns(string.Empty);
            var newEvolution = new MockTargetable();
            const int expected = 1;

            // Act
            var actual = new EvolutionLogic(_evolutionRepo.Object, _fileRepo.Object).Run(newEvolution);

            // Assert
            Assert.Equal(expected, actual);
            _evolutionRepo.Verify(v => v.ExecuteEvolution(It.IsAny<string>()), Times.Never);
            _evolutionRepo.Verify(v => v.AddEvolution(It.IsAny<Model.Evolution>(), It.IsAny<string>()), Times.Never);
        }
    }
}
