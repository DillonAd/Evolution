namespace Evolution.Model
{
    public interface IEvolutionCreatable : IEvolutionTargetable
    {
        string SourceFile { get; set; }
    }
}
