using System;
using Xunit;
using corex.@string.extensions;

namespace corex.@string.tests.extensions
{
    public class StringExtensionsTests
    {
        [Theory]
        [InlineData("-2147483648", int.MinValue)]
        [InlineData("-123", -123)]
        [InlineData("-1", -1)]
        [InlineData("0", 0)]
        [InlineData("1", 1)]
        [InlineData("123", 123)]
        [InlineData("2147483647", int.MaxValue)]
        public void WithValidIntegerAsString_ShouldReturnValidInteger(string input, int expectedOutput)
        {
            var actualOutput = input.To<int>();

            Assert.Equal(expectedOutput, actualOutput);
        }
    }
}