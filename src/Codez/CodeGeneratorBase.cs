using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Codez.Alphabets;
using Codez.Randomizers;
using Codez.StopWords;
using Codez.Transformers;
using Codez.Uniques;

namespace Codez
{
    public abstract class CodeGeneratorBase
    {
        protected readonly ITransformer transformer;
        protected readonly CodeGeneratorOptions options;
        protected readonly IAlphabet alphabet;
        protected readonly IRandomizer randomizer;
        protected readonly IUniqueness uniqueness;
        protected readonly IStopWords stopWords;
        protected readonly IReadOnlyList<IListener> listeners;

        protected CodeGeneratorBase(
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

        public virtual async ValueTask<string> GenerateAsync(int length)
        {
            var result = await TryGenerateAsync(length);

            if (result.Success)
                return result.Value;

            throw new CodeGeneratorException(result);
        }

        public virtual async ValueTask<CodeGeneratorResult> TryGenerateAsync(int length)
        {
            CodeGeneratorResult result = null;

            for (var retry = 1; retry <= options.RetryLimit; retry++)
            {
                await OnBeforeAttempt(new BeforeAttemptEvent(retry));

                var attempt = await GenerateAttemptAsync(length);

                result = await ValidateAttempt(retry, attempt);

                await OnAfterAttempt(new AfterAttemptEvent(result));

                if (result.Success)
                {
                    return result;
                }
            }

            return result ?? new CodeGeneratorResult();
        }

        protected async Task<CodeGeneratorResult> ValidateAttempt(int retry, string attempt)
        {
            var result = new CodeGeneratorResult
            {
                Reason = FailureReasonType.None,
                Retries = retry,
                Value = null
            };

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

            return result;
        }

        protected abstract Task<string> GenerateAttemptAsync(int length);

        protected virtual async Task OnBeforeAttempt(BeforeAttemptEvent @event)
        {
            var onBefore = listeners.Select(e => e.OnBeforeAttempt(@event));
            await Task.WhenAll(onBefore);
        }

        protected virtual async Task OnAfterAttempt(AfterAttemptEvent @event)
        {
            var onBefore = listeners.Select(e => e.OnAfterAttempt(@event));
            await Task.WhenAll(onBefore);
        }
    }
}