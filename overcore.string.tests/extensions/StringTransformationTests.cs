using System;
using Xunit;
using Xunit.Abstractions;
using overcore.@string.extensions;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Security.Cryptography;

namespace overcore.@string.tests.extensions
{
    public class StringTransformationTests
    {
        private readonly ITestOutputHelper output;

        public StringTransformationTests(ITestOutputHelper output)
        {
            this.output = output;
        }
        
        [Theory]
        [ClassData(typeof(HashHexTestData))]
        public void ComputeHashHexTests<T>(string input, T hashAlgorithm, Encoding encoding, string expectedHashHex) where T : HashAlgorithm, new()
        {
            var actualHashHex = input.ComputeHashHex<T>(hashAlgorithm, encoding);
            output.WriteLine($"{hashAlgorithm.GetType().Name} hash hex of \"{input}\": {actualHashHex}");
            Assert.Equal(expectedHashHex, actualHashHex);
        }
    }

    public class HashHexTestData : IEnumerable<object[]>
    {
        private static Encoding defaultEncoding = Encoding.UTF8;
        private const string inputToHash = "hello world";
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { inputToHash, new MD5CryptoServiceProvider(), defaultEncoding, "5eb63bbbe01eeed093cb22bb8f5acdc3" };
            yield return new object[] { inputToHash, new SHA1Managed(), defaultEncoding, "2aae6c35c94fcfb415dbe95f408b9ce91ee846ed" };
            yield return new object[] { inputToHash, new SHA256Managed(), defaultEncoding, "b94d27b9934d3e08a52e52d7da7dabfac484efe37a5380ee9088f7ace2efcde9" };
            yield return new object[] { inputToHash, new SHA384Managed(), defaultEncoding, "fdbd8e75a67f29f701a4e040385e2e23986303ea10239211af907fcbb83578b3e417cb71ce646efd0819dd8c088de1bd" };
            yield return new object[] { inputToHash, new SHA512Managed(), defaultEncoding, "309ecc489c12d6eb4cc40f50c902f2b4d0ed77ee511a7c7a9bcd3ca86d4cd86f989dd35bc5ff499670da34255b45b0cfd830e81f605dcf7dc5542e93ae9cd76f" };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}