using Evolution.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Evolution.Test.Integration.SqlClient
{
    public class MsSqlTestData : ITestData
    {
        public string FilePath => "./TestSql/MSSql/";
        public string User => "appUser";
        public string Password => "appPassword1";
        public string Server => "localhost";
        public string Instance => "evolutionDB";
        public string Port => "1433";
        public string Type => ((int)DatabaseTypes.MSSql).ToString();
        public bool UseConfig { get; }

        public MsSqlTestData(bool useConfig)
        {
            UseConfig = useConfig;
        }
    }
}
