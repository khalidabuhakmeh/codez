using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codez.Alphabets;
using Codez.Randomizers;
using Codez.StopWords;
using Codez.Transformers;
using Codez.Uniques;

namespace Codez
{
    public class CodeGenerator
    {
        private readonly ITransformer transformer;
        private readonly CodeGeneratorOptions options;
        private readonly IAlphabet alphabet;
        private readonly IRandomizer randomizer;
        private readonly IUniqueness uniqueness;
        private readonly IStopWords stopWords;
        private readonly IReadOnlyList<IListener> listeners;

        public CodeGenerator(
            CodeGeneratorOptions options = null,
            IAlphabet alphabet = null ,
            IRandomizer randomizer = null,
            IUniqueness uniqueness = null, 
            IStopWords stopWords = null,
            ITransformer transformer = null
        )
        {
            this.transformer = transformer;
            this.options = options ?? new CodeGeneratorOptions();
            this.alphabet = alphabet ?? new AsciiAlphabet();
            this.randomizer = randomizer ?? new RandomRandomizer();
            this.uniqueness = uniqueness ?? new NoUniqueness();
            this.stopWords = stopWords ?? new NoStopWords();

            listeners = new object[]
                {
                    this.alphabet,
                    this.randomizer,
                    this.uniqueness,
                    this.stopWords
                }
                .Where(x => x is IListener)
                .Cast<IListener>()
                .ToList();
        }

        public async ValueTask<string> GenerateAsync(int length)
        {
            var result = await TryGenerateAsync(length);

            if (result.Success)
                return result.Value;
            
            throw new CodeGeneratorException(result);
        }

        public async ValueTask<CodeGeneratorResult> TryGenerateAsync(int length)
        {
            var result = new CodeGeneratorResult();
            var sb = new StringBuilder();
            
            for (var retry = 1; retry <= options.RetryLimit; retry++)
            {
                await OnBeforeAttempt(new BeforeAttemptEvent(retry));

                for (var i = 0; i < length; i++)
                {
                    var characterCount = alphabet.Count;
                    var index = await randomizer.NextAsync(characterCount);
                    var character = alphabet.Get(index);
                    
                    sb.Append(character);
                }

                var attempt = sb.ToString();

                result.Reason = FailureReasonType.None;
                result.Retries = retry;
                result.Value = null;
               
                if (await stopWords.IsAllowedAsync(attempt))
                {
                    if (await uniqueness.IsUniqueAsync(attempt))
                    {
                        result.Value = attempt;
                        result.Success = true;
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
                
                if (transformer != null && result.Success)
                {
                    result = await transformer.Transform(result);
                }
                
                await OnAfterAttempt(new AfterAttemptEvent(result));

                if (result.Success)
                {
                    return result;
                }

                sb.Clear();
            }

            return result;
        }

        private async Task OnBeforeAttempt(BeforeAttemptEvent @event)
        {
            var onBefore = listeners.Select(e => e.OnBeforeAttempt(@event));
            await Task.WhenAll(onBefore);
        }
        
        private async Task OnAfterAttempt(AfterAttemptEvent @event)
        {
            var onBefore = listeners.Select(e => e.OnAfterAttempt(@event));
            await Task.WhenAll(onBefore);
        }
    }
}