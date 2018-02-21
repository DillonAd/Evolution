using Evolution.Model;

namespace Evolution.Repo
{
    public interface IEvolutionRepo
    {
        IProgression[] GetExecutedEvolutions();
        void AddEvolution(IProgression evolution);
        void RemoveEvolution(IProgression evolution);
        void SaveChanges();
        void ExecuteEvolution(string content);
    }
}
