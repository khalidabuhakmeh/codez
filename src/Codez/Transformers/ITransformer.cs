using System.Threading.Tasks;

namespace Codez.Transformers
{
    public interface ITransformer
    {
        ValueTask<CodeGeneratorResult> Transform(CodeGeneratorResult result);
    }
}