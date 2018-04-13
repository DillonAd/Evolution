using Evolution.Model;
using System;

namespace Evolution.Logic
{
    public interface IEvolutionLogic
    {
        int Run(IEvolutionCreatable newEvolution);
        int Run(IEvolutionTargetable targetedEvolution);
    }
}
