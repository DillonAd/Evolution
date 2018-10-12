using Evolution.Model;

namespace Evolution.Test.Unit.UnitTests.Infrastructure.Options
{
    public class MockCreatable : IEvolutionCreatable
    {
        private const string DefaultMockSourceFile = "MockSourceFile";

        private const string DefaultMockTargetEvolution = "MockTargetEvolution";

        public string TargetEvolution { get; set; }
        public string SourceFile { get; set; }

        public MockCreatable()
        {
            TargetEvolution = DefaultMockTargetEvolution;
            SourceFile = DefaultMockSourceFile;
        }
    }
}
