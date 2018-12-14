using System;
using System.Text;
using System.Threading.Tasks;
using Codez.Alphabets;
using Codez.Randomizers;
using Xunit;
using Xunit.Abstractions;

namespace Tests
{
    public class ExclusiveStringAlphabetTests
    {
        private readonly ITestOutputHelper output;

        public ExclusiveStringAlphabetTests(ITestOutputHelper output)
        {
            this.output = output;
        }
        
        [Fact]
        public async Task ExclusiveCharacterAlphabet_works()
        {
            var alphabet = new ExclusiveStringAlphabet("abcde");
            var sb = new StringBuilder();
            var random = new RandomRandomizer();
            
            for (int i = 0; i < alphabet.Characters.Count; i++)
            {
                var index = await random.NextAsync(alphabet.Count);
                var character = alphabet.Get(index);
                sb.Append(character);
            }

            var result = sb.ToString();
            
            output.WriteLine(result);
            
            Assert.Equal(5, result.Length);
        }
        
        [Fact]
        public async Task ExclusiveCharacterAlphabet_throws_when_runs_out()
        {
            var alphabet = new ExclusiveStringAlphabet("abcde");
            var random = new RandomRandomizer();

            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
                async () =>
                {
                    for (int i = 0; i < 10; i++)
                    {
                        var index = await random.NextAsync(alphabet.Count);
                        _ = alphabet.Get(index);
                    }
                }
            );
        }
    }
}