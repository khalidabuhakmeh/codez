using System;
using System.Threading.Tasks;

namespace Codez
{
    public class NonrepeatingCodeGenerator : CodeGeneratorBase, ICodeGenerator
    {
        public override ValueTask<CodeGeneratorResult> TryGenerateAsync(int length)
        {
            throw new NotImplementedException();
        }
    }
}
