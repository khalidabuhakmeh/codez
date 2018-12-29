using System.Linq;
using System.Threading.Tasks;
using Codez.Alphabets;
using Codez.Randomizers;
using Codez.StopWords;
using Codez.Transformers;
using Codez.Uniques;

namespace Codez.Generators
{
    public class NonRepeatingCodeGenerator : CodeGeneratorBase, ICodeGenerator
    {
        public NonRepeatingCodeGenerator(CodeGeneratorOptions options = null,
                             IAlphabet alphabet = null,
                             IRandomizer randomizer = null,
                             IUniqueness uniqueness = null,
                             IStopWords stopWords = null,
                             ITransformer transformer = null
        )
            : base(options, alphabet, randomizer, uniqueness, stopWords, transformer)
        {
        }

        public override ValueTask<CodeGeneratorResult> TryGenerateAsync(int length)
        {
            if (alphabet.Count < length)
            {
                return new ValueTask<CodeGeneratorResult>(new CodeGeneratorResult
                {
                    Reason = FailureReasonType.RequestInvalid,
                    Success = false
                });
            }

            return base.TryGenerateAsync(length);
        }

        protected override async Task<string> GenerateAttemptAsync(int length)
        {
            var shuffledAlphabet = await alphabet.Characters.ShuffleAsync(randomizer);

            return new string(shuffledAlphabet.Take(length).ToArray());
        }
    }
}
