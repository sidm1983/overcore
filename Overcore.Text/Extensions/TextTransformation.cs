using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Overcore.Text.Extensions
{
    /// <summary>
    /// A class containing extension methods that assist in various string transformation scenarios.
    /// </summary>
    public static class TextTransformation
    {
        /// <summary>
        /// Given an input string, a hash algorithm implementation and an optional encoding,
        /// this extension method will return a byte array containing the resultant bytes from hashing the input string using the hash algorithm supplied.
        /// </summary>
        /// <param name="input">The input string for which a hash needs to be computed</param>
        /// <param name="encoding">Optionally, the encoding to use during the conversion. If an encoding is not supplied, UTF8 will be used as the default encoding.</param>
        /// <typeparam name="T">The type of the hashing algorithm implementation class</typeparam>
        /// <returns>A hash of the input as a byte array</returns>
        public static byte[] ComputeHash<T>(this string input, Encoding encoding = null) where T : HashAlgorithm, new()
        {
            var inputBytes = input.ToByteArray(encoding);
            byte[] hashBytes;
            using(var hashImpl = new T())
            {
                hashBytes = hashImpl.ComputeHash(inputBytes);
            }
            return hashBytes;
        }

        /// <summary>
        /// Given an input string, a hash algorithm implementation and an optional encoding,
        /// this extension method will return a string containing the hash of the input string in hexadecimal format.
        /// </summary>
        /// <param name="input">The input string for which a hash needs to be computed</param>
        /// <param name="encoding">Optionally, the encoding to use during the conversion. If an encoding is not supplied, UTF8 will be used as the default encoding.</param>
        /// <typeparam name="T">The type of the hashing algorithm implementation class</typeparam>
        /// <returns>A hash of the input as a string in hexadecimal format.</returns>
        public static string ComputeHashHex<T>(this string input, Encoding encoding = null) where T : HashAlgorithm, new()
        {
            var hashBytes = ComputeHash<T>(input, encoding);
            return string.Concat(hashBytes.Select(x => x.ToString("x2")));
        }
    }
}