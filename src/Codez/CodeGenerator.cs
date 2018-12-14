using System.Collections;
using System.Text;
using System.Threading.Tasks;
using Codez.Alphabets;
using Codez.Randomizers;
using Codez.StopWords;
using Codez.Uniques;

namespace Codez
{
    public class CodeGenerator
    {
        private readonly CodeGeneratorOptions options;
        private readonly IAlphabet alphabet;
        private readonly IRandomizer randomizer;
        private readonly IUniqueness uniqueness;
        private readonly IStopWords stopWords;

        public CodeGenerator(
            CodeGeneratorOptions options = null,
            IAlphabet alphabet = null ,
            IRandomizer randomizer = null,
            IUniqueness uniqueness = null, 
            IStopWords stopWords = null
        )
        {
            this.options = options;
            this.alphabet = alphabet ?? new AsciiAlphabet();
            this.randomizer = randomizer ?? new RandomRandomizer();
            this.uniqueness = uniqueness ?? new NoUniqueness();
            this.stopWords = stopWords ?? new NoStopWords();
            this.options = new CodeGeneratorOptions();
        }

        public async ValueTask<string> Generate(int length)
        {
            var result = await TryGenerate(length);

            if (result.Success)
                return result.Value;
            
            throw new CodeGeneratorException($"Reached Retry Limit of {options.RetryLimit}");
        }

        public async ValueTask<CodeGeneratorResult> TryGenerate(int length)
        {
            var result = new CodeGeneratorResult();
            var sb = new StringBuilder();
            var alphabetLength = alphabet.Characters.Count;
            
            for (var retry = 1; retry <= options.RetryLimit; retry++)
            {                
                for (var i = 0; i < length; i++)
                {
                    var index = await randomizer.NextAsync(alphabetLength);
                    var character = alphabet.Characters[index];
                    sb.Append(character);
                }

                var attempt = sb.ToString();

                result.Reason = FailureReasonType.None;
                result.Retries = retry;

                if (await stopWords.IsAllowedAsync(attempt))
                {
                    if (await uniqueness.IsUniqueAsync(attempt))
                    {
                        result.Value = attempt;
                        result.Success = true;
                        return result;
                    }
                    else
                    {
                        result.Reason = FailureReasonType.Uniqueness;                        
                    }
                }
                else
                {
                    result.Reason = FailureReasonType.Stopped;
                }

                sb.Clear();
            }

            return result;
        }
    }
}