using System;
using Codez;
using Codez.Alphabets;
using Xunit;

namespace Tests
{
    public class StringAlphabetTests
    {
        [Fact]
        public void Can_construct_StringAlphabet()
        {
            var sut = new StringAlphabet("12345");
            
            Assert.Equal(5, sut.Characters.Count);
        }

        [Fact]
        public void Can_not_initialize_with_null()
        {
            Assert.Throws<ArgumentNullException>(() =>  new StringAlphabet(null));
        }
    }
}