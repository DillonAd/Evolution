using Evolution.Data.Entity;

namespace Evolution.Repo
{
    public interface IEvolutionRepo
    {
        string[] GetExecutedEvolutionFileNames();
        void AddEvolution(string name, string file, string content);
        void SaveChanges();
        void ExecuteEvolution(string content);
    }
}
