using Evolution.Data;
using Evolution.Exceptions;
using Evolution.Repo;
using Moq;
using System;
using System.Collections.Generic;
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
            mockContext.Setup(c => c.GetMigrationFileContent(It.IsAny<string>())).Returns(content);

            var repo = new FileRepo(mockContext.Object);
            var contentResult = repo.GetMigrationFileContent(direction);

            Assert.NotNull(contentResult);
            Assert.NotEmpty(contentResult);
            Assert.Equal(content, contentResult);
        }
    }
}
