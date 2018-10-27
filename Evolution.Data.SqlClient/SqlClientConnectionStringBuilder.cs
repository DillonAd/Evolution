using System.Data.SqlClient;

namespace Evolution.Data.SqlClient 
{
    public class SqlClientConnectionBuilder : IConnectionStringBuilder 
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

        public string Instance
        {
            get
            {
                return _Builder.InitialCatalog;
            }
            set
            {
                _Builder.InitialCatalog = value;
            }
        }

        public string Server
        {
            get
            {
                return _Builder.DataSource;
            }
            set
            {
                _Builder.DataSource = value;
            }
        }

        public int Port { get; set; }

        #endregion

        private readonly SqlConnectionStringBuilder _Builder;

        public SqlClientConnectionBuilder() 
        {
            _Builder = new SqlConnectionStringBuilder();
        }

        public string CreateConnectionString() => _Builder.ToString();
    }
}