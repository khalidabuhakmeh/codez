using Codez;

using Xunit.Abstractions;

namespace Tests
{
    class NonrepeatingCodeGeneratorTests : CodeGeneratorTestsBase<NonrepeatingCodeGenerator>
    {
        public NonrepeatingCodeGeneratorTests(ITestOutputHelper output) 
            : base(output)
        {

        }
    }
}
