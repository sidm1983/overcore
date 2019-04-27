using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace overcore.@string.extensions
{
    /// <summary>
    /// A class containing extension methods that assist in various string transformation scenarios.
    /// </summary>
    public static class StringTransformation
    {
        /// <summary>
        /// Given an input string, a hash algorithm implementation and an optional encoding,
        /// this extension method will return a byte array containing the resultant bytes from hashing the input string using the hash algorithm supplied.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="hashAlgorithm"></param>
        /// <param name="encoding"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns>A hash of the input as a byte array.</returns>
        public static byte[] ComputeHash<T>(this string input, T hashAlgorithm, Encoding encoding = null) where T : HashAlgorithm, new()
        {
            var inputBytes = (encoding ?? Encoding.UTF8).GetBytes(input);
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
        /// <param name="input"></param>
        /// <param name="hashAlgorithm"></param>
        /// <param name="encoding"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns>A hash of the input in hexadecimal format.</returns>
        public static string ComputeHashHex<T>(this string input, T hashAlgorithm, Encoding encoding = null) where T : HashAlgorithm, new()
        {
            var hashBytes = ComputeHash(input, hashAlgorithm, encoding);
            return string.Concat(hashBytes.Select(x => x.ToString("x2")));
        }
    }
}