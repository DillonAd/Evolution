using Evolution.Data;
using Evolution.Repo;
using Moq;
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

            var context = SetupFileContext(fileList);
            var repo = new FileRepo(context);
            repo.CreateMigrationFiles(migrationName);

            Assert.NotEmpty(fileList);
            Assert.Equal(2, fileList.Count);
            Assert.Contains(migrationName, fileList[0]);
            Assert.Contains(migrationName, fileList[1]);
        }

        public IFileContext SetupFileContext(List<string> fileList)
        {
            var mockContext = new Mock<IFileContext>();
            mockContext.Setup(c => c.CreateFile(It.IsAny<string>())).Callback<string>(fileName => fileList.Add(fileName));

            return mockContext.Object;
        }
    }
}
