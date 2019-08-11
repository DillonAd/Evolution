using Evolution.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Evolution.Test.Integration.Oracle
{
    public class OracleTestData : ITestData
    {
        public string FilePath => "./TestSql/Oracle/";
        public string User => "c##appUser";
        public string Password => "appPassword";
        public string Server => "localhost";
        public string Instance => "ORCLCDB";
        public string Port => "6666";
        public string Type => ((int)DatabaseTypes.Oracle).ToString();
        public bool UseConfig { get; }

        public OracleTestData(bool useConfig)
        {
            UseConfig = useConfig;
        }
    }
}
