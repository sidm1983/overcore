using System;
using Xunit;
using Overcore.Text.Extensions;
using System.Text;
using System.Collections.Generic;
using System.Collections;
using System.Globalization;

namespace Overcore.Text.Tests.Extensions
{
    public class TextConversionTests
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
        public void CallingTo_WithValidInputString_ReturnsValidOutput<T>(string input, T expectedOutput)
            => Assert.Equal(expectedOutput, input.To<T>());
        
        [Fact]
        public void CallingTo_WithNullInputString_ThrowsException()
            => Assert.Throws<InvalidCastException>(() => TextConversion.To<bool>(null));

        [Fact]
        public void CallingTo_WithInvalidFormatInputString_ThrowsException()
            => Assert.Throws<FormatException>(() => "hello".To<int>());

        [Fact]
        public void CallingTo_WithOverflowFormatInputString_ThrowsException()
            => Assert.Throws<OverflowException>(() => "9223372036854775807".To<int>());

        [Theory]
        [InlineData("25.50", "en-AU", 25.5)]
        [InlineData("40.000,5", "id-ID", 40000.5)]
        [InlineData("12,34.50", "hi-IN", 1234.5)]
        public void CallingTo_WithValidInputStringAndFormatProvider_ReturnsValidOutput<T>(string input, string cultureName, T expectedOutput)
        {
            var cultureInfo = CultureInfo.GetCultureInfo(cultureName);
            Assert.Equal(expectedOutput, input.To<T>(cultureInfo));
        }
        
        [Theory]
        [InlineData("true", false, true)]
        [InlineData("5.0", 10.0D, 5.0D)]
        [InlineData("0.2", 0.1f, 0.2f)]
        [InlineData("123", 0, 123)]
        public void CallingTo_WithValidInputStringAndDefaultValue_ReturnsValidOutputAndNotDefaultValue<T>(string input, T defaultValue, T expectedOutput)
            => Assert.Equal(expectedOutput, input.To<T>(defaultValue));

        [Theory]
        [InlineData(null, false)]
        [InlineData("", 10.0D)]
        [InlineData("hello", 0.1f)]
        [InlineData("9223372036854775807", -1)]
        public void CallingTo_WithInvalidInputStringAndDefaultValue_ReturnsDefaultValue<T>(string input, T defaultValue)
            => Assert.Equal(defaultValue, input.To<T>(defaultValue));

        [Theory]
        [ClassData(typeof(ToByteArrayTestData))]
        public void ToByteArrayTests(string input, Encoding encoding, byte[] expectedOutput)
            => Assert.Equal(expectedOutput, input.ToByteArray(encoding));
    }

    public class ToByteArrayTestData : IEnumerable<object[]>
    {
        private static readonly Encoding defaultEncoding = Encoding.UTF8;

        private static readonly Dictionary<string, byte[]> stringBytes = new Dictionary<string, byte[]>
        {
            //Arabic
            { "مرحبا", new byte[]{217,133,216,177,216,173,216,168,216,167} },
            //Chinese
            { "你好", new byte[]{228,189,160,229,165,189} },
            //Emoji
            { "👋", new byte[]{240,159,145,139} },
            //English
            { "hello", new byte[]{104,101,108,108,111} },
            //Greek
            { "γειά", new byte[]{206,179,206,181,206,185,206,172} },
            //Hebrew
            { "שלום", new byte[]{215,169,215,156,215,149,215,157} },
            //Hindi
            { "नमस्ते", new byte[]{224,164,168,224,164,174,224,164,184,224,165,141,224,164,164,224,165,135} },
            //Japanese
            { "世界", new byte[]{228,184,150,231,149,140} },
            //Korean
            { "세계", new byte[]{236,132,184,234,179,132} },
            //Portuguese
            { "Olá", new byte[]{79,108,195,161} },
            //Russian
            { "мир", new byte[]{208,188,208,184,209,128} },
            //Empty string
            { string.Empty, Array.Empty<byte>() }
        };

        public IEnumerator<object[]> GetEnumerator()
        {
            foreach(var s in stringBytes)
            {
                yield return new object[] { s.Key, defaultEncoding, s.Value };
            }

            //Adding in a 'null' string as a test.
            yield return new object[] { null, defaultEncoding, Array.Empty<byte>() };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}