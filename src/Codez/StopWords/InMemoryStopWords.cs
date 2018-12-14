using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codez.StopWords
{
    public class InMemoryStopWords : IStopWords
    {
        private readonly IEnumerable<string> words;
        private readonly bool ignoreCase;

        public InMemoryStopWords(params string[] words)
            :this(words, true)
        {            
        }

        public InMemoryStopWords(IEnumerable<string> words, bool ignoreCase = true)
        {
            this.words = words ?? throw new ArgumentNullException(nameof(words));
            this.ignoreCase = ignoreCase;
        }
        
        public ValueTask<bool> IsAllowedAsync(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            
            var comparison = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
            var found = words.Any(stop => value.IndexOf(value, comparison ) > -1);

            return new ValueTask<bool>(!found);
        }
    }
}