using Evolution.Data.Entity;

namespace Evolution.Repo
{
    public interface IEvolutionRepo
    {
        string[] GetExecutedEvolutionFileNames();
        void AddEvolution(Model.Evolution evolution, string content);
        void ExecuteEvolution(string content);
    }
}
