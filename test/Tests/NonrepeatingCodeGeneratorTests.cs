using Codez;

using Xunit.Abstractions;

namespace Tests
{
    public class NonrepeatingCodeGeneratorTests : CodeGeneratorTestsBase<NonrepeatingCodeGenerator>
    {
        public NonrepeatingCodeGeneratorTests(ITestOutputHelper output) 
            : base(output)
        {

        }
    }
}
