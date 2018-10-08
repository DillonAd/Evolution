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
        [Fact]
        [Trait("Category", "unit")]
        public void RunEvolutionCreatable_WhenEvolutionFileCreatedSuccessfully()
        {
            // Arrange
            var fileRepo = new Mock<IFileRepo>();
            var evolutionRepo = new Mock<IEvolutionRepo>();
            fileRepo.Setup(s => s.CreateEvolutionFile(It.IsAny<Model.Evolution>(), It.IsAny<string>())).Verifiable();
            var newEvolution = new MockCreatable();
            const int expected = 0;

            // Act
            var actual = new EvolutionLogic(evolutionRepo.Object, fileRepo.Object).Run(newEvolution);

            // Assert
            Assert.Equal(expected, actual);
            fileRepo.Verify(v => v.CreateEvolutionFile(It.IsAny<Model.Evolution>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        [Trait("Category", "unit")]
        public void RunEvolutionCreatable_WhenEvolutionFileCreationThrowsException()
        {
            // Arrange
            var fileRepo = new Mock<IFileRepo>();
            var evolutionRepo = new Mock<IEvolutionRepo>();
            fileRepo.Setup(s => s.CreateEvolutionFile(It.IsAny<Model.Evolution>(), It.IsAny<string>())).Throws(new Exception());
            var newEvolution = new MockCreatable();
            const int expected = 1;

            // Act
            var actual = new EvolutionLogic(evolutionRepo.Object, fileRepo.Object).Run(newEvolution);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        [Trait("Category", "unit")]
        public void RunEvolutionTargetable_WhenUnexecutedEvolutionsExist_ThenEvolutionFilesCreated()
        {
            // Arrange
            var fileRepo = new Mock<IFileRepo>();
            var evolutionRepo = new Mock<IEvolutionRepo>();
            evolutionRepo.Setup(s => s.GetExecutedEvolutionFileNames()).Returns(new[] {"date_ExFile1", "date_ExFile2"});
            evolutionRepo.Setup(s => s.ExecuteEvolution(It.IsAny<string>())).Verifiable();
            evolutionRepo.Setup(s => s.AddEvolution(It.IsAny<Model.Evolution>(), It.IsAny<string>())).Verifiable();
            fileRepo.Setup(s => s.GetUnexecutedEvolutionFiles(It.IsAny<string[]>())).Returns(new[] {"date_UnExFile1"});
            fileRepo.Setup(s => s.GetEvolutionFileContent(It.IsAny<Model.Evolution>())).Returns(string.Empty);
            var newEvolution = new MockTargetable();
            const int expected = 0;

            // Act
            var actual = new EvolutionLogic(evolutionRepo.Object, fileRepo.Object).Run(newEvolution);

            // Assert
            Assert.Equal(expected, actual);
            evolutionRepo.Verify(v => v.ExecuteEvolution(It.IsAny<string>()), Times.Once);
            evolutionRepo.Verify(v => v.AddEvolution(It.IsAny<Model.Evolution>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        [Trait("Category", "unit")]
        public void RunEvolutionTargetable_WhenNoUnexecutedEvolutionsExist_ThenEvolutionFilesCreated()
        {
            // Arrange
            var fileRepo = new Mock<IFileRepo>();
            var evolutionRepo = new Mock<IEvolutionRepo>();
            evolutionRepo.Setup(s => s.GetExecutedEvolutionFileNames()).Returns(new[] { "date_ExFile1", "date_ExFile2" });
            evolutionRepo.Setup(s => s.ExecuteEvolution(It.IsAny<string>())).Verifiable();
            evolutionRepo.Setup(s => s.AddEvolution(It.IsAny<Model.Evolution>(), It.IsAny<string>())).Verifiable();
            fileRepo.Setup(s => s.GetUnexecutedEvolutionFiles(It.IsAny<string[]>())).Returns(new string[] {});
            fileRepo.Setup(s => s.GetEvolutionFileContent(It.IsAny<Model.Evolution>())).Returns(string.Empty);
            var newEvolution = new MockTargetable();
            const int expected = 0;

            // Act
            var actual = new EvolutionLogic(evolutionRepo.Object, fileRepo.Object).Run(newEvolution);

            // Assert
            Assert.Equal(expected, actual);
            evolutionRepo.Verify(v => v.ExecuteEvolution(It.IsAny<string>()), Times.Never);
            evolutionRepo.Verify(v => v.AddEvolution(It.IsAny<Model.Evolution>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        [Trait("Category", "unit")]
        public void RunEvolutionTargetable_WhenExceptionThrown_ThenResultIsCorrect()
        {
            // Arrange
            var fileRepo = new Mock<IFileRepo>();
            var evolutionRepo = new Mock<IEvolutionRepo>();
            evolutionRepo.Setup(s => s.GetExecutedEvolutionFileNames()).Returns(new string[] { });
            evolutionRepo.Setup(s => s.ExecuteEvolution(It.IsAny<string>())).Verifiable();
            evolutionRepo.Setup(s => s.AddEvolution(It.IsAny<Model.Evolution>(), It.IsAny<string>())).Verifiable();
            fileRepo.Setup(s => s.GetUnexecutedEvolutionFiles(It.IsAny<string[]>())).Returns(new[] { "UnExFile1" });
            fileRepo.Setup(s => s.GetEvolutionFileContent(It.IsAny<Model.Evolution>())).Returns(string.Empty);
            var newEvolution = new MockTargetable();
            const int expected = 1;

            // Act
            var actual = new EvolutionLogic(evolutionRepo.Object, fileRepo.Object).Run(newEvolution);

            // Assert
            Assert.Equal(expected, actual);
            evolutionRepo.Verify(v => v.ExecuteEvolution(It.IsAny<string>()), Times.Never);
            evolutionRepo.Verify(v => v.AddEvolution(It.IsAny<Model.Evolution>(), It.IsAny<string>()), Times.Never);
        }
    }
}
