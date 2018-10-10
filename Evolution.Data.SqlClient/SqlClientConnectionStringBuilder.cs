using System.Data.SqlClient;

namespace Evolution.Data.SqlClient {
    public class SqlClientConnectionBuilder : IConnectionStringBuilder {
        #region Properties

        public string UserName {
            get {
                return _Builder.UserID;
            }
            set {
                _Builder.UserID = value;
            }
        }

        public string Password {
            get {
                return _Builder.Password;
            }
            set {
                _Builder.Password = value;
            }
        }

        public string Instance { get; set; }

        public string Server { get; set; }

        public int Port { get; set; }

        #endregion

        private readonly SqlConnectionStringBuilder _Builder;

        public SqlClientConnectionBuilder () {
            _Builder = new SqlConnectionStringBuilder ();
        }

        public string CreateConnectionString () {
            _Builder.DataSource = $"Server={Server};Database={Instance};User Id={UserName};Password = {Password}; ";
            return _Builder.ToString ();
        }
    }
}