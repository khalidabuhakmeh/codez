using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            var result = await sut.Generate(length);
                        
            output.WriteLine(result);
            
            Assert.NotNull(result);
            Assert.Equal(length, result.Length);
        }

        [Fact]
        public async Task Can_hit_retry_limit_with_exception()
        {
            var generator = new CodeGenerator(uniqueness: new Never());
            await Assert.ThrowsAsync<CodeGeneratorException>(async() => await generator.Generate(1));
        }
        
        [Fact]
        public async Task Can_hit_retry_limit_with_boolean()
        {
            var generator = new CodeGenerator(uniqueness: new Never());
            var result = await generator.TryGenerate(1);
            
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

            var result = await generator.TryGenerate(1);
            
            Assert.False(result.Success);
            Assert.Equal(FailureReasonType.Stopped, result.Reason);
        }

        public class Predictable : IAlphabet
        {
            public IReadOnlyList<char> Characters { get; } = new ReadOnlyCollection<char>(new[] {'A'});
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