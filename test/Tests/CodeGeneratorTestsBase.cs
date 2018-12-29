using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Codez;
using Codez.Alphabets;
using Codez.Generators;
using Codez.StopWords;
using Codez.Uniques;

using Xunit;
using Xunit.Abstractions;

namespace Tests
{
    public abstract class CodeGeneratorTestsBase<T> where T: ICodeGenerator
    {
        private readonly ITestOutputHelper output;
        protected readonly CodeGeneratorTestContext _c = new CodeGeneratorTestContext();

        protected CodeGeneratorTestsBase(ITestOutputHelper output)
        {
            this.output = output;
        }
        
        [Fact]
        public async Task Can_generate_a_code()
        {
            var generator = _c.CreateGenerator<T>();

            const int length = 5;
            var result = await generator.GenerateAsync(length);
                        
            output.WriteLine(result);
            
            Assert.NotNull(result);
            Assert.Equal(length, result.Length);
        }

        [Fact]
        public async Task Can_hit_retry_limit_with_exception()
        {
            var generator = _c.CreateGenerator<T>(uniqueness: new Never());
            await Assert.ThrowsAsync<CodeGeneratorException>(async() => await generator.GenerateAsync(1));
        }
        
        [Fact]
        public async Task Can_hit_retry_limit_with_boolean()
        {
            var generator = _c.CreateGenerator<T>(uniqueness: new Never());
            var result = await generator.TryGenerateAsync(1);
            
            Assert.Null(result.Value);
            Assert.False(result.Success);
        }

        [Fact]
        public async Task Can_hit_stop_word()
        {
            var generator = _c.CreateGenerator<T>(
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
            var generator = _c.CreateGenerator<T>(
                alphabet: new ExclusiveStringAlphabet("abcdefg123456789")
            );

            var result = await generator.GenerateAsync(5);
            
            output.WriteLine(result);
            
            Assert.Equal(5, result.Length);

            var allCharactersAreUnique = result.All(character => result.Count(current => current == character) == 1);
            Assert.True(allCharactersAreUnique);          
        }

        [Fact]
        public async Task Can_generate_no_repeating_codes()
        {
            var generator = _c.CreateGenerator<T>(
                new CodeGeneratorOptions { RetryLimit = int.MaxValue },
                uniqueness: new InMemoryUniqueness(),
                alphabet: new StringAlphabet("0123456789")
            );

            var results = new List<string>();

            for (var i = 0; i < 5; i++)
            {
                var result = await generator.GenerateAsync(1);
                output.WriteLine(result);
                results.Add(result);              
            }
            
            Assert.Equal(5, results.Count);
        }

        public class Predictable : IAlphabet
        {
            public char Get(int index)
            {
                return Characters[0];
            }

            public int Count => Characters.Count;

            public IReadOnlyList<char> Characters { get; } = new List<char> { 'A' };
        }

        public class Never : IUniqueness
        {
            public ValueTask<bool> IsUniqueAsync(string value)
            {
                return new ValueTask<bool>(false);
            }
        }
    }
}