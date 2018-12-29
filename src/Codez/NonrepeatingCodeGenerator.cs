using System;
using System.Threading.Tasks;

namespace Codez
{
    public class NonrepeatingCodeGenerator : ICodeGenerator
    {
        public ValueTask<string> GenerateAsync(int length)
        {
            throw new NotImplementedException();
        }

        public ValueTask<CodeGeneratorResult> TryGenerateAsync(int length)
        {
            throw new NotImplementedException();
        }
    }
}
