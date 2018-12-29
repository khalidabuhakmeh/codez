using System.Threading.Tasks;

namespace Codez
{
    public interface ICodeGenerator
    {
        ValueTask<string> GenerateAsync(int length);
        ValueTask<CodeGeneratorResult> TryGenerateAsync(int length);
    }
}