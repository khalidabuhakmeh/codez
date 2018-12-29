using System.Threading.Tasks;
using Codez.Generators;

namespace Codez.Transformers
{
    public interface ITransformer
    {
        ValueTask<CodeGeneratorResult> Transform(CodeGeneratorResult result);
    }
}