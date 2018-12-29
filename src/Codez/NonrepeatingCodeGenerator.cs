using System;
using System.Threading.Tasks;

namespace Codez
{
    public class NonrepeatingCodeGenerator : CodeGeneratorBase, ICodeGenerator
    {
        protected override async Task<string> GenerateAttemptAsync(int length)
        {
            throw new NotImplementedException();
        }
    }
}
