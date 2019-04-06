using System;
using System.Collections.Generic;
[assembly: CLSCompliant(true)]
namespace corex.@string.extensions
{
    public static class StringConversion
    {
        /// <summary>
        /// This extension method converts a string to a Common Language Runtime (CLR) type that has an equivalent value.
        /// </summary>
        /// <param name="input">The string to convert</param>
        /// <typeparam name="T">The CLR type into which the input string will be converted</typeparam>
        /// <returns>An instance of the CLR type that has an equivalent value to the input string</returns>
        public static T To<T>(this string input)
        {
            return (T) Convert.ChangeType(input, typeof(T));
        }
    }
}
