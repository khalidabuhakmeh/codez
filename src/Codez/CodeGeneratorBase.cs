using System;
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

        public abstract ValueTask<string> GenerateAsync(int length);

        public abstract ValueTask<CodeGeneratorResult> TryGenerateAsync(int length);

        protected async Task OnBeforeAttempt(BeforeAttemptEvent @event)
        {
            var onBefore = listeners.Select(e => e.OnBeforeAttempt(@event));
            await Task.WhenAll(onBefore);
        }

        protected async Task OnAfterAttempt(AfterAttemptEvent @event)
        {
            var onBefore = listeners.Select(e => e.OnAfterAttempt(@event));
            await Task.WhenAll(onBefore);
        }
    }
}