using Evolution.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Evolution.Test.Unit.IntegrationTests.SqlClient
{
    public sealed class EvolutionTest : IDisposable
    {
        private const string _FilePath = "./IntegrationTests/TestSql/MSSql/";

        private readonly ITestOutputHelper _outputHelper;

        public EvolutionTest(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Fact]
        [Trait("Category", "integration")]
        public void Evolve_Success()
        {
            var fileList = Directory.GetFiles(_FilePath).OrderBy(file => file);
            string arguments;

            foreach (var fileName in fileList)
            {
                arguments = $"{ GetAddArguments(fileName) } { GetConnectionOptionsString() }";
                Assert.Equal(0, Run(arguments));
            }

            string targetEvolution;

            foreach (var fileName in fileList)
            {
                targetEvolution = fileName.Replace(".sql", string.Empty).Replace(_FilePath, string.Empty);
                arguments = $"{ GetExecArguments(targetEvolution) } { GetConnectionOptionsString() }";
                _outputHelper.WriteLine(arguments);
                Assert.Equal(0, Run(arguments));
            }
        }

        [Fact]
        [Trait("Category", "integration")]
        public void Evolve_Success_With_Config()
        {
            WriteConfig(GetConnectionOptionsString());

            var fileList = Directory.GetFiles(_FilePath).OrderBy(file => file);
            string arguments;

            foreach (var fileName in fileList)
            {
                arguments = $"{ GetAddArguments(fileName) }";
                Assert.Equal(0, Run(arguments));
            }

            string targetEvolution;

            foreach (var fileName in fileList)
            {
                targetEvolution = fileName.Replace(".sql", string.Empty).Replace(_FilePath, string.Empty);
                arguments = $"{ GetExecArguments(targetEvolution) }";
                _outputHelper.WriteLine(arguments);
                Assert.Equal(0, Run(arguments));
            }
        }

        private int Run(string arguments)
        {
            foreach(var arg in arguments.Split(' '))
                _outputHelper.WriteLine(arg);

            return Program.Main(arguments.Split(' '));
        }

        private string GetAddArguments(string fileName)
        {
            return string.Format("add --target {0} --source {1}",
                Path.GetFileName(fileName).Replace(".sql", string.Empty),
                fileName);
        }

        private string GetExecArguments(string targetName)
        {
            return string.Format("exec --target {0}",targetName);
        }

        private string GetConnectionOptionsString()
        {
            var connectionParams = $"--user {TestContext.Parameters["MSSqlUser"]} " +
                $"--password {TestContext.Parameters["MSSqlPassword"]} " +
                $"--server {TestContext.Parameters["MSSqlServer"]} " +
                $"--instance {TestContext.Parameters["MSSqlInstance"]} " +
                $"--port {TestContext.Parameters["MSSqlPort"]} " +
                $"--type {((int)DatabaseTypes.MSSql).ToString()}";
            
            return connectionParams;
        }

        private void WriteConfig(string connectionString)
        {
            var options = connectionString.Split(" ");
            var filePath = Path.Combine(Environment.CurrentDirectory, ".evo");
            string key;
            string value;

            File.WriteAllText(filePath, "");

            for(int i = 0; i < options.Length; i++)
            {
                key = i <= options.Length ? options[i].Replace("--", "") : string.Empty;
                value = i <= options.Length ? options[++i] : string.Empty;
                File.AppendAllText(filePath, $"{key}={value}\n");
            }
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
            { "MSSqlUser", "c##appUser" },
            { "MSSqlPassword", "appPassword" },
            { "MSSqlServer", "localhost" },
            { "MSSqlInstance", "MSSQLSERVER" },
            { "MSSqlPort", "1433" }
        };
    }
}
