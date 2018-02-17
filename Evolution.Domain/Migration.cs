namespace Evolution.Domain
{
    public class Migration : IMigration
    {
        public string Name { get; set; }

        public Migration(string name)
        {
            Name = name;
        }
    }
}
