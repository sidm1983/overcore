using System;
using System.Collections.Generic;
using System.Globalization;
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
        /// This method uses the invariant culture during conversion. Please use the IFormatProvider overload to specify either the system's current culture or any other culture as desired.
        /// </summary>
        /// <param name="input">The string to convert</param>
        /// <typeparam name="T">The CLR type into which the input string will be converted</typeparam>
        /// <returns>An object of the specified generic type that has an equivalent value to the input string</returns>
        public static T To<T>(this string input)
            => To<T>(input, CultureInfo.InvariantCulture);

        /// <summary>
        /// This extension method converts a string to a Common Language Runtime (CLR) type that has an equivalent value using the specified format provider.
        /// </summary>
        /// <param name="input">The string to convert</param>
        /// <param name="provider">An IFormatProvider object that supplies culture-specific formatting information</param>
        /// <typeparam name="T">The CLR type into which the input string will be converted</typeparam>
        /// <returns>An object of the specified generic type that has an equivalent value to the input string</returns>
        public static T To<T>(this string input, IFormatProvider provider)
        {
            //Getting the underlying type is needed for generic support.
            //For example, if we want to convert the string to the generic type Nullable<T>.
            var finalType = GetUnderlyingType(typeof(T));
            return (T) Convert.ChangeType(input, finalType, provider);
        }

        /// <summary>
        /// This extension method converts a string to a Common Language Runtime (CLR) type that has an equivalent value.
        /// Additionally, this method also accepts a default value which is returned if the string cannot be converted to the specified type.
        /// This method uses the invariant culture during conversion. Please use the IFormatProvider overload to specify either the system's current culture or any other culture as desired.
        /// </summary>
        /// <param name="input">The string to convert</param>
        /// <param name="defaultValue">A user-supplied default value to return if conversion fails</param>
        /// <typeparam name="T">The CLR type into which the input string will be converted</typeparam>
        /// <returns>An object of the specified generic type that has an equivalent value to the input string or the supplied default value if conversion fails.</returns>
        public static T To<T>(this string input, T defaultValue)
            => To<T>(input, CultureInfo.InvariantCulture, defaultValue);

        /// <summary>
        /// Converts a string to a Common Language Runtime (CLR) type that has an equivalent value.
        /// Additionally, this method accepts a formatting provider and a default value which is returned if the string cannot be converted to the specified type using the specified formatting provider.
        /// </summary>
        /// <param name="input">The string to convert</param>
        /// <param name="provider">An IFormatProvider object that supplies culture-specific formatting information</param>
        /// <param name="defaultValue">A user-supplied default value to return if conversion fails</param>
        /// <typeparam name="T">The CLR type into which the input string will be converted</typeparam>
        /// <returns>An object of the specified generic type that has an equivalent value to the input string or the supplied default value if conversion fails.</returns>
        public static T To<T>(this string input, IFormatProvider provider, T defaultValue)
        {
            try
            {
                var output = To<T>(input, provider);

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
        
        /// <summary>
        /// This method returns the underlying type for a generic type.
        /// </summary>
        /// <param name="type">The type for which to return the underlying type</param>
        /// <returns>The underlying type being used by the generic type or the type itself if it isn't a generic type</returns>
        private static Type GetUnderlyingType(this Type type)
        {
            if(type == null || !type.IsGenericType)
            {
                return type;
            }
            
            return type.GetGenericArguments()[0];
        }
    }
}
