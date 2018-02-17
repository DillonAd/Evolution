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
            var fileList = new List<string>();

            var context = SetupFileContext();
            var repo = new FileRepo(context);
            repo.CreateMigrationFiles();

            Assert.NotEmpty(fileList);
            Assert.Equal(2, fileList.Count);
        }

        public IFileContext SetupFileContext(List<string> fileList)
        {
            var mockContext = new Mock<IFileContext>();
            mockContext.Setup(c => c.CreateFile(It.IsAny<string>)).Callback<string>(fileName => fileList.Add(fileName));

            return mockContext.Object;
        }
    }
}
