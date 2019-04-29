using System;
using Xunit;
using Xunit.Abstractions;
using Overcore.Text.Extensions;
using System.Linq;

namespace Overcore.Text.Tests.Extensions
{
    public class TextGenerationTests
    {
        const string alphaNumericCharSet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        const int validLength = 10;
        const int invalidLength = -1;
        
        private readonly ITestOutputHelper output;

        public TextGenerationTests(ITestOutputHelper output)
        {
            this.output = output;
        }
        
        [Fact]
        public void GenerateRandomString_WithInvalidLength_ThrowsException() => Assert.Throws<ArgumentOutOfRangeException>(() => alphaNumericCharSet.GenerateRandomString(invalidLength));

        [Fact]
        public void GenerateRandomString_WithInvalidCharacterSet_ThrowsException() => Assert.Throws<ArgumentException>(() => "".GenerateRandomString(validLength));

        [Fact]
        public void GenerateRandomString_WithValidCharacterSetAndValidLength_ReturnsStringWithValidLength()
        {
            var randomString = alphaNumericCharSet.GenerateRandomString(validLength);
            Assert.Equal(validLength, randomString.Length);
        }

        [Theory]
        [InlineData(alphaNumericCharSet, 16)]
        [InlineData("10", 8)]
        [InlineData("abcdefghijklmnopqrstuvwxyz", 1)]
        [InlineData("a", 10)]
        public void GenerateRandomString_WithValidCharacterSetAndValidLength_ReturnsStringWithValidCharacters(string charset, int length)
        {
            var randomString = charset.GenerateRandomString(length);
            var validCharsUsed = randomString.All(c => charset.IndexOf(c) >= 0);
            output.WriteLine($"Generated random string: {randomString}");
            Assert.True(validCharsUsed);
        }
    }
}