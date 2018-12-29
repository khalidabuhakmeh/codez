using System.Collections.Generic;
using System.Threading.Tasks;

namespace Codez.Uniques
{
    public class InMemoryUniqueness : IUniqueness, IListener
    {
        private readonly HashSet<string> existing = new HashSet<string>();

        public ValueTask<bool> IsUniqueAsync(string value)
        {
            return new ValueTask<bool>(!existing.Contains(value));
        }

        public Task OnBeforeAttempt(BeforeAttemptEvent @event)
        {
            return Task.FromResult(0);
        }

        public Task OnAfterAttempt(AfterAttemptEvent @event)
        {
            if (@event.Result.Success)
            {
                existing.Add(@event.Result.Value);
            }

            return Task.FromResult(0);
        }
    }
}