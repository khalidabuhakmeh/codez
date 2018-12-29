using System.Threading.Tasks;

using Codez;
using Codez.Alphabets;
using Codez.Generators;
using Xunit;
using Xunit.Abstractions;

namespace Tests
{
    public class NonrepeatingCodeGeneratorTests : CodeGeneratorTestsBase<NonRepeatingCodeGenerator>
    {
        public NonrepeatingCodeGeneratorTests(ITestOutputHelper output) 
            : base(output)
        {

        }

        [Fact]
        public async Task Cannot_create_a_code_longer_than_the_alphabet()
        {
            var alphabet = new AsciiAlphabet();
            var length = alphabet.Count + 1;

            var generator = _c.CreateGenerator<NonRepeatingCodeGenerator>(alphabet: alphabet);

            var result = await generator.TryGenerateAsync(length);

            Assert.False(result.Success);
            Assert.Equal(FailureReasonType.RequestInvalid, result.Reason);
        }
    }
}
