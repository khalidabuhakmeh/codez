
using Codez;

using Xunit.Abstractions;

namespace Tests
{
    public class CodeGeneratorTests : BaseCodeGeneratorTests<CodeGenerator>
    {
        public CodeGeneratorTests(ITestOutputHelper output) 
            : base(output)
        {
        }
    }
}