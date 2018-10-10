using CommandLine;
using Evolution.Model;

namespace Evolution.Options
{
    public abstract class DatabaseAuthenticationOptions : IDatabaseAuthenticationOptions
    {
        [Option("user", HelpText = "Database UserName")]
        public string User { get; set; }

        [Option("password", HelpText = "Database Password")]
        public string Password { get; set; }

        [Option("server", HelpText = "Database Server Name")]
        public string Server { get; set; }

        [Option("instance", HelpText = "Database Instance Name")]
        public string Instance { get; set; }

        [Option("port", HelpText = "Database Port")]
        public int Port { get; set; }

        [Option("type", HelpText = DB_HELP_TEXT)]
        public DatabaseTypes Type { get; set; }

        private const string DB_HELP_TEXT = "Type of database.\n" +
            "  Valid Options:\n" +
            "    1 - Oracle\n" +
            "    2 - MSSql";
    }
}
