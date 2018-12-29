using System.Linq;
using System.Threading.Tasks;

using Codez.Alphabets;
using Codez.Randomizers;
using Codez.StopWords;
using Codez.Transformers;
using Codez.Uniques;

namespace Codez
{
    public class NonrepeatingCodeGenerator : CodeGeneratorBase, ICodeGenerator
    {
        public NonrepeatingCodeGenerator(CodeGeneratorOptions options = null,
                             IAlphabet alphabet = null,
                             IRandomizer randomizer = null,
                             IUniqueness uniqueness = null,
                             IStopWords stopWords = null,
                             ITransformer transformer = null
        )
            : base(options, alphabet, randomizer, uniqueness, stopWords, transformer)
        {
        }

        protected override async Task<string> GenerateAttemptAsync(int length)
        {
            var shuffledAlphabet = await alphabet.Characters.ShuffleAsync(randomizer);

            return new string(shuffledAlphabet.Take(length).ToArray());
        }
    }
}
