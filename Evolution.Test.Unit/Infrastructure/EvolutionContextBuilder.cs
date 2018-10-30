using Evolution.Data;
using Evolution.Data.Entity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolution.Test.Unit.Infrastructure
{
    public class EvolutionContextBuilder
    {
        public IEvolutionContext Context => _Mock.Object;

        private readonly Mock<IEvolutionContext> _Mock;

        public EvolutionContextBuilder()
        {
            _Mock = new Mock<IEvolutionContext>();
        }

        public EvolutionContextBuilder AddGetEvolutionBehavior(List<IEvolution> evolutions)
        {
            _Mock.Setup(cm => cm.GetEvolutions()).Returns(evolutions);

            return this;
        }
    }
}
