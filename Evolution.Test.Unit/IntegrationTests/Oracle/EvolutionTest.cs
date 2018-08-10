using Evolution.Model;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace Evolution.Test.Unit.IntegrationTests.Oracle
{
    public class EvolutionTest : IDisposable
    {
        private const string _FilePath = "./IntegrationTests/TestSql/Oracle/";

        [Fact]
        [Trait("Category", "integration")]
        public void Evolve_Success()
        {
            var fileList = Directory.GetFiles(_FilePath);

            foreach (var fileName in fileList)
            {
                Assert.Equal(0, Add(fileName));
            }

            string targetEvolution;

            foreach (var fileName in fileList)
            {
                targetEvolution = fileName.Replace(fileName, ".sql").Replace(_FilePath, string.Empty);
                Assert.Equal(0, Execute(targetEvolution));
            }
        }

        private int Add(string fileName)
        {
            var executionString = string.Format("add {0} --target {1} --source {2}",
                GetConnectionOptionsString(),
                Path.GetFileName(fileName).Replace(".sql", string.Empty),
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
            var connectionParams = string.Format("--user {0} --pwd {1} --server {2} --instance {3} --port {4} --type {5}",
                TestContext.Parameters["OracleUser"],
                TestContext.Parameters["OraclePassword"],
                TestContext.Parameters["OracleServer"],
                TestContext.Parameters["OracleInstance"],
                TestContext.Parameters["OraclePort"],
                ((int)DatabaseTypes.Oracle).ToString());

            return connectionParams;
        }

        public void Dispose()
        {
            foreach(var file in Directory.GetFiles("./", "*.sql", SearchOption.TopDirectoryOnly))
            {
                File.Delete(file);
            }
        }
    }

    internal static class TestContext
    {
        public static Dictionary<string, string> Parameters { get; } = new Dictionary<string, string>()
        {
            { "OracleUser", "c##appUser" },
            { "OraclePassword", "appPassword" },
            { "OracleServer", "localhost" },
            { "OracleInstance", "ORCLCDB.localdomain" },
            { "OraclePort", "6666" }
        };
    }
}
