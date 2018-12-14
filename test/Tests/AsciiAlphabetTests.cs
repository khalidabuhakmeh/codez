using System.Linq;
using Codez;
using Codez.Alphabets;
using Xunit;

namespace Tests
{
    public class AsciiAlphabetTests
    {
        private readonly AsciiAlphabet sut = new AsciiAlphabet();
        
        [Fact]
        public void Starts_with_a_bang()
        {
            var actual = sut.Characters.First();
            Assert.Equal('!', actual);
        }

        [Fact]
        public void Ends_with_a_whisper()
        {
            var actual = sut.Characters.Last();
            Assert.Equal('~', actual);
        }        
    }
}