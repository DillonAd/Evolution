using Evolution.Data.Entity;

namespace Evolution.Repo
{
    public interface IEvolutionRepo
    {
        string[] GetExecutedEvolutionFileNames();
        void AddEvolution(Model.Evolution evolution, string content);
        void SaveChanges();
        void ExecuteEvolution(string content);
    }
}
