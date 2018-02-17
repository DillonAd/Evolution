using Evolution.Data;
using Evolution.Domain;
using Evolution.Exceptions;
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
        public void CreateMigrationFiles_Success()
        {
            var migrationName = "migration1";
            var fileList = new List<string>();

            var mockContext = new Mock<IFileContext>();
            mockContext.Setup(c => c.CreateFile(It.IsAny<string>())).Callback<string>(fileName => fileList.Add(fileName));

            var repo = new FileRepo(mockContext.Object);
            repo.CreateMigrationFiles(migrationName);

            Assert.NotEmpty(fileList);
            Assert.Equal(2, fileList.Count);
            Assert.Contains(migrationName, fileList[0]);
            Assert.Contains(migrationName, fileList[1]);
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
                    throw new MigrationFileException("Migration file already exists");
                }
                else
                {
                    fileList.Add(fileName);
                }
            });
            mockContext.Setup(c => c.DeleteFile(It.IsAny<string>())).Callback<string>(fileName => fileList.Remove(fileName));

            var repo = new FileRepo(mockContext.Object);

            Assert.Throws<MigrationFileException>(() => repo.CreateMigrationFiles(migrationName));
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
            var contentResult = repo.GetMigrationFileContent(fileName);

            Assert.NotNull(contentResult);
            Assert.NotEmpty(contentResult);
            Assert.Equal(content, contentResult);
        }

        [Fact]
        public void GetUnexecutedMigrations()
        {
            var executedMigrations = new Migration[]
            {
                new Migration("Migration1"),
                new Migration("Migration2"),
                new Migration("Migration3")
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
            string[] unexecutedMigrationFilesResult = repo.GetUnexecutedMigrations(executedMigrations).ToArray();

            Assert.NotEmpty(unexecutedMigrationFilesResult);
            Assert.Equal(unexecutedMigrationFiles, unexecutedMigrationFilesResult);
        }
    }
}
