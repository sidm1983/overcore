using System;
using System.Linq;
using System.Security.Cryptography;

namespace overcore.@string.extensions
{
    /// <summary>
    /// A class containing extension methods that assist in various string generation scenarios.
    /// </summary>
    public static class StringGeneration
    {
        /// <summary>
        /// This extension method accepts a list of characters as a string and a length and returns a random, crytographically secure string of the specified length.
        /// </summary>
        /// <param name="characterSet">A string containing a set of characters to use for string generation.</param>
        /// <param name="length">The length of the string to generate.</param>
        /// <returns></returns>
        public static string GenerateRandomString(this string characterSet, int length)
        {
            const int byteMultiplier = 8;
            if(length <= 0 || length > int.MaxValue / byteMultiplier)
                throw new ArgumentOutOfRangeException("length", $"The specified length must be a positive, non-zero 32-bit integer with a maximum value of {int.MaxValue / byteMultiplier}.");
            
            if(string.IsNullOrEmpty(characterSet))
                throw new ArgumentException("The character set cannot be null or empty.", "characterSet");
            
            var characterSetArray = characterSet.Distinct().ToArray();

            //Adding in a short circuit path for this function for performance purposes.
            //If the character set array only contains 1 character, then the rest of the code becomes overkill.
            //Let's just return a string with the specified length where all the characters are the same
            //as specified by the single character passed in to the character set.
            if(characterSetArray.Length == 1)
            {
                return new string(characterSetArray[0], length);
            }

            var randomBytes = new byte[length * byteMultiplier];
            var outputCharacters = new char[length];

            using(var rngProvider = new RNGCryptoServiceProvider())
            {
                //Let's generate a random set of bytes from the crypto service provider.
                rngProvider.GetBytes(randomBytes);
            }

            for(var i = 0; i < length; i++)
            {
                //Let's extract a 64-bit integer (8 bytes at a time) from the random set of bytes.
                ulong longInt = BitConverter.ToUInt64(randomBytes, i * byteMultiplier);

                //We can now pick out a random character using a modulus operation to make sure
                //that we only ever select a character from the range of characters available in the character set array.
                outputCharacters[i] = characterSetArray[longInt % (uint)characterSetArray.Length];
            }
            
            return new string(outputCharacters);
        }
    }
}