namespace Evolution.Data
{
    public interface IConnectionStringBuilder
    {
        string UserName { get; set; }
        string Password { get; set; }
        string Instance { get; set; }
        string Server { get; set; }

        string CreateConnectionString();
    }
}
