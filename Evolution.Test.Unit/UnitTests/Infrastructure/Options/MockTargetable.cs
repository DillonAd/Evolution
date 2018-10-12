using Evolution.Model;

namespace Evolution.Test.Unit.UnitTests.Infrastructure.Options
{
    public class MockTargetable : IEvolutionTargetable
    {
        private const string DefaultMockTargetEvolution = "MockTargetEvolution";

        public string TargetEvolution { get; set; }

        public MockTargetable()
        {
            TargetEvolution = DefaultMockTargetEvolution;
        }
    }
}
