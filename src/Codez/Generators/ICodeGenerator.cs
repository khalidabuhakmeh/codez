using System.Threading.Tasks;

namespace Codez.Generators
{
    public interface ICodeGenerator
    {
        ValueTask<string> GenerateAsync(int length);
        ValueTask<CodeGeneratorResult> TryGenerateAsync(int length);
    }
}