using System;
using System.Collections.Generic;
using System.Text;

[assembly: CLSCompliant(true)]
namespace Overcore.Text.Extensions
{
    /// <summary>
    /// A class containing extension methods that convert a string into other types.
    /// </summary>
    public static class TextConversion
    {
        /// <summary>
        /// This extension method converts a string to a Common Language Runtime (CLR) type that has an equivalent value.
        /// </summary>
        /// <param name="input">The string to convert</param>
        /// <typeparam name="T">The CLR type into which the input string will be converted</typeparam>
        /// <returns>An object of the specified generic type that has an equivalent value to the input string</returns>
        public static T To<T>(this string input)
            => (T) Convert.ChangeType(input, typeof(T));

        /// <summary>
        /// This extension method converts a string to a Common Language Runtime (CLR) type that has an equivalent value using the specified format provider.
        /// </summary>
        /// <param name="input">The string to convert</param>
        /// <param name="provider">An object that supplies culture-specific formatting information</param>
        /// <typeparam name="T">The CLR type into which the input string will be converted</typeparam>
        /// <returns>An object of the specified generic type that has an equivalent value to the input string</returns>
        public static T To<T>(this string input, IFormatProvider provider)
            => (T) Convert.ChangeType(input, typeof(T), provider);

        /// <summary>
        /// This extension method converts a string to a Common Language Runtime (CLR) type that has an equivalent value.
        /// Additionally, this method also accepts a default value which is returned if the string cannot be converted to the specified type.
        /// </summary>
        /// <param name="input">The string to convert</param>
        /// <param name="defaultValue">A user-supplied default value to return if conversion fails.</param>
        /// <typeparam name="T">The CLR type into which the input string will be converted</typeparam>
        /// <returns>An object of the specified generic type that has an equivalent value to the input string or the supplied default value if conversion fails.</returns>
        public static T To<T>(this string input, T defaultValue)
        {
            try
            {
                var output = To<T>(input);

                if (output != null)
                {
                    return output;
                }
            }
            catch
            {
                //Intentionally ignoring any exceptions as we just want it to return the default value that was passed in if an exception does occur.
            }

            return defaultValue;
        }

        /// <summary>
        /// Converts a string to a byte array using the specified encoding.
        /// If an encoding is not supplied, it will default to using UTF8 encoding.
        /// </summary>
        /// <param name="input">The string to convert</param>
        /// <param name="encoding">Optionally, the encoding to use during the conversion. If an encoding is not supplied, UTF8 will be used as the default encoding.</param>
        /// <returns>An array of bytes that represent the value of the string as per the encoding</returns>
        public static byte[] ToByteArray(this string input, Encoding encoding = null)
            => (encoding ?? Encoding.UTF8).GetBytes(input ?? string.Empty);
    }
}
