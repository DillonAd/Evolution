using Evolution.Repo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Evolution.Domain
{
    public class Evolution
    {
        private readonly IEvolutionRepo _Repo;

        public Evolution(IEvolutionRepo repo)
        {
            _Repo = repo;
        }
    }
}
