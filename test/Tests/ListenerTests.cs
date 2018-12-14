using System.Collections.Generic;
using System.Threading.Tasks;
using Codez;
using Codez.Alphabets;
using Xunit;

namespace Tests
{
    public class ListenerTests
    {
        [Fact]
        public async Task Can_listen_for_attempts()
        {
            var alphabet = new CountingAlphabet();
            var generator = new CodeGenerator(alphabet: alphabet);
            
            Assert.Equal(0, alphabet.Count);

            await generator.GenerateAsync(5);
            
            Assert.Equal(1, alphabet.Count);
        }
    }

    public class CountingAlphabet : StringAlphabet, IListener
    {
        public CountingAlphabet()
        :  base("ABCDE")
        {            
        }
        
        public IReadOnlyList<char> Characters { get; }
        public Task OnBeforeAttempt(BeforeAttemptEvent @event)
        {
            Count = @event.Attempt;
            return Task.FromResult(0);
        }

        public int Count { get; private set; }

        public Task OnAfterAttempt(AfterAttemptEvent @event)
        {
            return Task.FromResult(0);
        }
    }
}