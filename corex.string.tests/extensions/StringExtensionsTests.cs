using System;
using Xunit;
using corex.@string.extensions;

namespace corex.@string.tests.extensions
{
    public enum Status
    {
        Ready,
        InProgress,
        Done
    }
    
    public class StringExtensionsTests
    {
        [Theory]
        [InlineData("-128",                     SByte.MinValue)]
        [InlineData("127",                      SByte.MaxValue)]
        [InlineData("0",                        byte.MinValue)]
        [InlineData("255",                      byte.MaxValue)]
        [InlineData("-32768",                   short.MinValue)]
        [InlineData("32767",                    short.MaxValue)]
        [InlineData("0",                        ushort.MinValue)]
        [InlineData("65535",                    ushort.MaxValue)]
        [InlineData("-2147483648",              int.MinValue)]
        [InlineData("2147483647",               int.MaxValue)]
        [InlineData("0",                        uint.MinValue)]
        [InlineData("4294967295",               uint.MaxValue)]
        [InlineData("-9223372036854775808",     long.MinValue)]
        [InlineData("9223372036854775807",      long.MaxValue)]
        [InlineData("0",                        ulong.MinValue)]
        [InlineData("18446744073709551615",     ulong.MaxValue)]
        [InlineData("3.14159",                  3.14159f)]
        [InlineData("-3.14159",                 -3.14159f)]
        [InlineData("3.14159",                  3.14159D)]
        [InlineData("-3.14159",                 -3.14159D)]
        [InlineData("true",                     true)]
        [InlineData("false",                    false)]
        [InlineData("x",                        'x')]
        public void ValidInputString_ReturnsValidOutput<T>(string input, T expectedOutput) => Assert.Equal(expectedOutput, input.To<T>());

        [Fact]
        public void InvalidFormatInputString_ThrowsException() => Assert.Throws<FormatException>(() => "hello".To<int>());

        [Fact]
        public void OverflowFormatInputString_ThrowsException() => Assert.Throws<OverflowException>(() => "9223372036854775807".To<int>());
    }
}