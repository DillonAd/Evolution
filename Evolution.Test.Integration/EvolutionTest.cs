using Evolution.Model;
using Evolution.Test.Integration.Oracle;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Evolution.Test.Integration.SqlClient
{
    public sealed class EvolutionTest : IDisposable
    {
        private readonly ITestOutputHelper _outputHelper;

        public EvolutionTest(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Theory]
        [MemberData(nameof(TestData))]
        [Trait("Category", "integration")]
        public void Evolve_Success(ITestData testData)
        {
            var fileList = Directory.GetFiles(testData.FilePath).OrderBy(file => file);
            string arguments;

            if(testData.UseConfig)
            {
                WriteConfig(GetConnectionOptionsString(testData));    
            }

            foreach (var fileName in fileList)
            {
                arguments = $"{ GetAddArguments(fileName) } { GetConnectionOptionsString(testData) }";
                Assert.Equal(0, Run(arguments));
            }

            string targetEvolution;

            foreach (var fileName in fileList)
            {
                targetEvolution = fileName.Replace(".sql", string.Empty).Replace(testData.FilePath, string.Empty);
                arguments = $"{ GetExecArguments(targetEvolution) } { GetConnectionOptionsString(testData) }";
                _outputHelper.WriteLine(arguments);
                Assert.Equal(0, Run(arguments));
            }
        }

        [Theory]
        [MemberData(nameof(TestData))]
        [Trait("Category", "integration")]
        public void Evolve_Success_With_Config(ITestData testData)
        {
            WriteConfig(GetConnectionOptionsString(testData));

            var fileList = Directory.GetFiles(testData.FilePath).OrderBy(file => file);
            string arguments;

            foreach (var fileName in fileList)
            {
                arguments = $"{ GetAddArguments(fileName) }";
                Assert.Equal(0, Run(arguments));
            }

            string targetEvolution;

            foreach (var fileName in fileList)
            {
                targetEvolution = fileName.Replace(".sql", string.Empty).Replace(testData.FilePath, string.Empty);
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

        private string GetConnectionOptionsString(ITestData testData)
        {
            var connectionParams = $"--user {testData.User} " +
                $"--password {testData.Password} " +
                $"--server {testData.Server} " +
                $"--instance {testData.Instance} " +
                $"--port {testData.Port} " +
                $"--type {testData.Type}";
            
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


        public static IEnumerable<object[]> TestData()
        {
            yield return new object[] { new OracleTestData(false) };
            yield return new object[] { new OracleTestData(true) };
            yield return new object[] { new MsSqlTestData(false) };
            yield return new object[] { new MsSqlTestData(true) };
        }     
    }
}
