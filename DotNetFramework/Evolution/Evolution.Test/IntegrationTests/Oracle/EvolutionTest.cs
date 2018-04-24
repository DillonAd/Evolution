using Evolution.Model;
using NUnit.Framework;
using System.IO;

namespace Evolution.Test.Unit.IntegrationTests.Oracle
{
    public class EvolutionTest
    {
        private const string _FilePath = "./IntegrationTests/TestSql/Oracle/";

        [Test]
        [Category("integration")]
        public void Evolve_Success()
        {
            var fileList = Directory.GetFiles(_FilePath);

            foreach(var fileName in fileList)
            {
                Assert.Zero(Add(fileName));
            }

            string targetEvolution;

            foreach(var fileName in fileList)
            {
                targetEvolution = fileName.Replace(fileName, ".sql").Replace(_FilePath, string.Empty);
                Assert.Zero(Execute(targetEvolution));
            }
        }

        private int Add(string fileName)
        {
            var executionString = string.Format("add {0} --target {1} --source {2}",
                GetConnectionOptionsString(),
                fileName.Replace(".sql", string.Empty),
                fileName);

            return Program.Main(executionString.Split(' '));
        }

        private int Execute(string targetEvolution)
        {
            var executionString = string.Format("exec {0} --target {1}",
                GetConnectionOptionsString(),
                targetEvolution);

            return Program.Main(executionString.Split(' '));
        }

        private string GetConnectionOptionsString()
        {
            return string.Format("--user {0} --pwd {1} --server {2} --instance {3} --port {4} --type {5}",
                TestContext.Parameters["OracleUser"],
                TestContext.Parameters["OraclePassword"],
                TestContext.Parameters["OracleServer"],
                TestContext.Parameters["OracleInstance"],
                TestContext.Parameters["OraclePort"],
                ((int)DatabaseTypes.Oracle).ToString());
        }
    }
}
