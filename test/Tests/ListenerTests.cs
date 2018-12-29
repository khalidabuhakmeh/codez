using System.Threading.Tasks;
using Codez;
using Codez.Alphabets;
using Codez.Generators;
using Codez.Listeners;
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
            
            Assert.Equal(0, alphabet.Attempts);

            await generator.GenerateAsync(5);
            
            Assert.Equal(1, alphabet.Attempts);
        }
    }

    public class CountingAlphabet : StringAlphabet, IListener
    {
        public CountingAlphabet()
        :  base("ABCDE")
        {            
        }
        
        public Task OnBeforeAttempt(BeforeAttemptEvent @event)
        {
            Attempts = @event.Attempt;
            return Task.FromResult(0);
        }

        public int Attempts { get; private set; }

        public Task OnAfterAttempt(AfterAttemptEvent @event)
        {
            return Task.FromResult(0);
        }
    }
}