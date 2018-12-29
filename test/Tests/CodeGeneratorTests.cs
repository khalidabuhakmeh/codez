
using Codez;

using Xunit.Abstractions;

namespace Tests
{
    public class CodeGeneratorTests : CodeGeneratorTestsBase<CodeGenerator>
    {
        public CodeGeneratorTests(ITestOutputHelper output) 
            : base(output)
        {
        }
    }
}