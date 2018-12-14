using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Codez;
using Codez.Alphabets;
using Codez.StopWords;
using Codez.Uniques;
using Xunit;
using Xunit.Abstractions;

namespace Tests
{
    public class CodeGeneratorTests
    {
        private readonly ITestOutputHelper output;
        private readonly CodeGenerator sut = new CodeGenerator();

        public CodeGeneratorTests(ITestOutputHelper output)
        {
            this.output = output;
        }
        
        [Fact]
        public async Task Can_generate_a_code()        
        {
            const int length = 5;
            var result = await sut.GenerateAsync(length);
                        
            output.WriteLine(result);
            
            Assert.NotNull(result);
            Assert.Equal(length, result.Length);
        }

        [Fact]
        public async Task Can_hit_retry_limit_with_exception()
        {
            var generator = new CodeGenerator(uniqueness: new Never());
            await Assert.ThrowsAsync<CodeGeneratorException>(async() => await generator.GenerateAsync(1));
        }
        
        [Fact]
        public async Task Can_hit_retry_limit_with_boolean()
        {
            var generator = new CodeGenerator(uniqueness: new Never());
            var result = await generator.TryGenerateAsync(1);
            
            Assert.Null(result.Value);
            Assert.False(result.Success);
        }

        [Fact]
        public async Task Can_hit_stop_word()
        {
            var generator = new CodeGenerator(
                options: new CodeGeneratorOptions { RetryLimit = 1 },
                alphabet: new Predictable(),
                stopWords: new InMemoryStopWords("A"),
                uniqueness: new NoUniqueness()
            );

            var result = await generator.TryGenerateAsync(1);
            
            Assert.False(result.Success);
            Assert.Equal(FailureReasonType.Stopped, result.Reason);
        }

        [Fact]
        public async Task Can_generate_character_unique_code()
        {
            var generator = new CodeGenerator(
                alphabet: new ExclusiveStringAlphabet("abcdefg123456789")
            );

            var result = await generator.GenerateAsync(5);
            
            output.WriteLine(result);
            
            Assert.Equal(5, result.Length);

            var allCharactersAreUnique = result.All(character => result.Count(current => current == character) == 1);
            Assert.True(allCharactersAreUnique);          
        }

        public class Predictable : IAlphabet
        {
            public char Get(int index)
            {
                return 'A';
            }

            public int Count => 1;
        }

        public class Never : IUniqueness
        {
            public async ValueTask<bool> IsUniqueAsync(string value)
            {
                return false;
            }
        }
    }
}