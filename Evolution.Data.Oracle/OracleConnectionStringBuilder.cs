using Oracle.ManagedDataAccess.Client;

namespace Evolution.Data.Oracle
{
    public class OracleConnectionBuilder : IConnectionStringBuilder
    {
        #region Properties

        public string UserName
        {
            get
            {
                return _Builder.UserID;
            }
            set
            {
                _Builder.UserID = value;
            }
        }

        public string Password
        {
            get
            {
                return _Builder.Password;
            }
            set
            {
                _Builder.Password = value;
            }
        }

        public string Instance { get; set; }

        public string Server { get; set; }

        public int Port { get; set; }

        #endregion

        private readonly OracleConnectionStringBuilder _Builder;

        public OracleConnectionBuilder()
        {
            _Builder = new OracleConnectionStringBuilder();
        }

        public string CreateConnectionString()
        {
            _Builder.DataSource = $"(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={Server})(PORT={Port}))(CONNECT_DATA=(SERVICE_NAME={Instance})))";
            return _Builder.ToString();
        }
    }
}
