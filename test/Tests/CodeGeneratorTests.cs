
using Codez;
using Codez.Generators;
using Xunit.Abstractions;

namespace Tests
{
    public class CodeGeneratorTests : CodeGeneratorTestsBase<DefaultCodeGenerator>
    {
        public CodeGeneratorTests(ITestOutputHelper output) 
            : base(output)
        {
        }
    }
}