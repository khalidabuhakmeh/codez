using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Codez.Listeners;

namespace Codez.Alphabets
{
    public class ExclusiveStringAlphabet : StringAlphabet, IListener
    {
        private readonly List<char> used = new List<char>();
        
        public ExclusiveStringAlphabet(string alphabet) 
            : base(alphabet)
        {
        }

        public override char Get(int index)
        {
            var remaining = Characters.Except(used).ToArray();
            
            if (remaining.Length == 0)
                throw new ArgumentOutOfRangeException(nameof(index), "alphabet has run out of unique characters");
            
            // don't exceed the index of what's left
            var max = Math.Min(index, remaining.Length - 1);
            var value = remaining[max];
            
            used.Add(value);

            return value;
        }

        public override int Count => Characters.Except(used).Count();

        public Task OnBeforeAttempt(BeforeAttemptEvent @event)
        {
            used.Clear();
            return Task.FromResult(0);
        }

        public Task OnAfterAttempt(AfterAttemptEvent @event)
        {
            return Task.FromResult(0);
        }
    }
}