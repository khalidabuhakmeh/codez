using Codez;

using Xunit.Abstractions;

namespace Tests
{
    public class NonrepeatingCodeGeneratorTests : BaseCodeGeneratorTests<NonrepeatingCodeGenerator>
    {
        public NonrepeatingCodeGeneratorTests(ITestOutputHelper output) 
            : base(output)
        {

        }
    }
}
