using Evolution.Data;
using Evolution.Data.Entity;
using Evolution.Logic;
using Evolution.Model;
using Evolution.Repo;
using StructureMap;

namespace Evolution.IoC
{
    public static class DependencyRegistry
    {
        public static IEvolutionLogic GetApplication(IDatabaseAuthenticationOptions dbAuthOptions)
        {
            return new Container(_ =>
            {
                _.For<IEvolutionContext>().Use(new DbContextFactory().CreateContext(dbAuthOptions));
                _.For<IFileContext>().Use<FileContext>();
                _.For<IFileRepo>().Use<FileRepo>();
                _.For<IEvolutionRepo>().Use<EvolutionRepo>();
                _.For<IEvolutionLogic>().Use<EvolutionLogic>();
                _.For<IEvolution>().Use<Data.Entity.Evolution>();
            }).GetInstance<IEvolutionLogic>();
        }
    }
}
