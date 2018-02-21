using Evolution.Data;
using Evolution.Exceptions;
using Evolution.Model;
using Evolution.Repo;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Evolution.Test.Unit
{
    public class FileRepoTests
    {
        [Fact]
        public void CreateMigrationFile_Success()
        {
            var migrationName = "migration1";
            var fileList = new List<string>();

            var mockContext = new Mock<IFileContext>();
            mockContext.Setup(c => c.CreateFile(It.IsAny<string>())).Callback<string>(fileName => fileList.Add(fileName));

            var repo = new FileRepo(mockContext.Object);
            repo.CreateEvolutionFile(migrationName);

            Assert.NotEmpty(fileList);
            Assert.Contains(migrationName, fileList[0]);
        }

        [Fact]
        public void CreateMigrationFiles_FileExists()
        {
            var migrationName = "migration1";
            var fileList = new List<string>();
            var count = 0;

            var mockContext = new Mock<IFileContext>();
            mockContext.Setup(c => c.CreateFile(It.IsAny<string>())).Callback<string>((fileName) =>
            {
                count++;

                if (count > 1)
                {
                    throw new EvolutionFileException("Migration file already exists");
                }
                else
                {
                    fileList.Add(fileName);
                }
            });
            mockContext.Setup(c => c.DeleteFile(It.IsAny<string>())).Callback<string>(fileName => fileList.Remove(fileName));

            var repo = new FileRepo(mockContext.Object);

            Assert.Throws<EvolutionFileException>(() => repo.CreateEvolutionFile(migrationName));
            Assert.Empty(fileList);
        }

        [Fact]
        public void GetMigrationFileContents()
        {
            var fileName = "migration1.up.sql";
            var content = "migration file content";

            var mockContext = new Mock<IFileContext>();
            mockContext.Setup(c => c.GetMigrationFileContent(It.Is<string>(f => f == fileName))).Returns(content);

            var repo = new FileRepo(mockContext.Object);
            var contentResult = repo.GetEvolutionFileContent(fileName);

            Assert.NotNull(contentResult);
            Assert.NotEmpty(contentResult);
            Assert.Equal(content, contentResult);
        }

        [Fact]
        public void GetUnexecutedMigrations()
        {
            var executedMigrations = new IProgression[]
            {
                new Progression() { Name = "Migration1" },
                new Progression() { Name = "Migration2" },
                new Progression() { Name = "Migration3" }
            };

            var unexecutedMigrationFiles = new string[]
            {
                "Migration4.up.sql",
                "Migration4.down.sql",
                "Migration5.up.sql",
                "Migration5.down.sql",
                "Migration6.up.sql",
                "Migration6.down.sql"
            };

            var migrationFileNames = new List<string>()
            {
                "Migration1.up.sql",
                "Migration1.down.sql",
                "Migration2.up.sql",
                "Migration2.down.sql",
                "Migration3.up.sql",
                "Migration3.down.sql"
            };
            migrationFileNames.AddRange(unexecutedMigrationFiles);

            var mockContext = new Mock<IFileContext>();
            mockContext.Setup(mc => mc.GetMigrationFileNames()).Returns(migrationFileNames.ToArray());

            var repo = new FileRepo(mockContext.Object);
            var unexecutedMigrationFilesResult = repo.GetUnexecutedEvolutions(executedMigrations).ToArray();

            Assert.NotEmpty(unexecutedMigrationFilesResult);
            //Assert.Equal(unexecutedMigrationFiles, unexecutedMigrationFilesResult);
        }
    }
}
