using Evolution.Data;
using Oracle.ManagedDataAccess.Client;
using System.Data.Common;

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

        #endregion

        private OracleConnectionStringBuilder _Builder;

        public OracleConnectionBuilder()
        {
            _Builder = new OracleConnectionStringBuilder();
        }

        public string CreateConnectionString()
        {
            //TODO Get Port
            _Builder.DataSource = string.Format("SERVER=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={0})(PORT={1}))" + 
                "(CONNECT_DATA=(SERVICE_NAME={2})))", Server, 1433, Instance);

            return _Builder.ToString();
        }
    }
}
