namespace Evolution.Model
{
    public interface IDatabaseAuthenticationOptions
    {
        string UserName { get; set; }
        string Password { get; set; }
        string Server { get; set; }
        string Instance { get; set; }
        int Port { get; set; }
        DatabaseTypes DatabaseType { get; set; }
    }
}
