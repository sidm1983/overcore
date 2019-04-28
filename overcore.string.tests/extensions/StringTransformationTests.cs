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

        private static string[] inputsToHash =
        {
            //Arabic
            "مرحبا بالعالم",
            //Chinese
            "你好世界",
            //Emoji
            "👋🌎",
            //English
            "hello world",
            //Greek
            "γειά σου κόσμος",
            //Hebrew
            "שלום עולם",
            //Hindi
            "नमस्ते दुनिया",
            //Japanese
            "こんにちは世界",
            //Korean
            "여보세요 세계",
            //Portuguese
            "Olá mundo",
            //Russian
            "Здравствуй, мир",
        };

        private static HashAlgorithm[] hashAlgorithms =
        {
            new MD5CryptoServiceProvider(),
            new SHA1Managed(),
            new SHA256Managed(),
            new SHA384Managed(),
            new SHA512Managed()
        };

        private static string[,] hashMatrix =
        {
            //Arabic: "مرحبا بالعالم"
            {
                //MD5
                "6dae813a78afab836a3eb6023d07281e",
                //SHA1
                "01f4cee2f921466bd3fc5d2c7bbb7e9bacce4d5e",
                //SHA256
                "9262a0a791605071a500c1a15bef2d5efcc6c8f198567105e9ab364811377e9f",
                //SHA384
                "9464362d293c2534c8cc2787ca85fd2efd252cf77a69b0a1f1471fccf6fc18e0e99ba75ec1bac139a99959ce7284e451",
                //SHA512
                "02ab86aa5543c2e23a2f15fd16618df6cdd13d143a28d57a04ae12b8c9fd3c225ebc1e150bebdac658b744b7a158ac8d519804883cd6728207dc2230ae4a8913"
            },
            //Chinese: "你好世界" hashes
            {
                //MD5
                "65396ee4aad0b4f17aacd1c6112ee364",
                //SHA1
                "dabaa5fe7c47fb21be902480a13013f16a1ab6eb",
                //SHA256
                "beca6335b20ff57ccc47403ef4d9e0b8fccb4442b3151c2e7d50050673d43172",
                //SHA384
                "5621250177cc297c02d4c7c2a950d541a52b5c478e1fa16ca5022316de898d7be5c66b16ad735295b48b84a47e986144",
                //SHA512
                "4b28a152c8e203ebb52e099301041e3cf704a56190d3097ec8b086a0f9bfb4b9d533ce71fc3bcf374359e506dc5f17322ec3911eac8dd8f5b35308d938ba0c26"
            },
            //Emoji: "👋🌎" hashes
            {
                //MD5
                "0a01a7bd5dbb874cab64a7dafeb2d75d",
                //SHA1
                "a1ebd49177a2b59b91b2f1378750f435d3ff3c1c",
                //SHA256
                "9d74a427ad8719529c47e6fe20303536a79dedaa0da9cde4b53c17e95db935bc",
                //SHA384
                "b017001b6638c5f1dcfd7fc390622779f09e2c257e988a037ea0f84f8308122d99c8bc3537aa6f7cd2c6378d2c0651e1",
                //SHA512
                "8eb5b311f1c5d527c7a5457201b1af62437a11423d75491830d7fd066df6dca3334b697f168551a473aa412b34003f69562156ad6c520000249b77d3488da42e"
            },
            //English: "hello world" hashes
            {
                //MD5
                "5eb63bbbe01eeed093cb22bb8f5acdc3",
                //SHA1
                "2aae6c35c94fcfb415dbe95f408b9ce91ee846ed",
                //SHA256
                "b94d27b9934d3e08a52e52d7da7dabfac484efe37a5380ee9088f7ace2efcde9",
                //SHA384
                "fdbd8e75a67f29f701a4e040385e2e23986303ea10239211af907fcbb83578b3e417cb71ce646efd0819dd8c088de1bd",
                //SHA512
                "309ecc489c12d6eb4cc40f50c902f2b4d0ed77ee511a7c7a9bcd3ca86d4cd86f989dd35bc5ff499670da34255b45b0cfd830e81f605dcf7dc5542e93ae9cd76f"
            },
            //Greek: "γειά σου κόσμος" hashes
            {
                //MD5
                "88d327981d40a31de74e3616ab89e9d0",
                //SHA1
                "2f593ef919ca1d7cf960750e547a6de9943d37a5",
                //SHA256
                "6ecae015749963645303ae7da9407a015cc4686b6d3e7daaf25b140ff3a85f87",
                //SHA384
                "eca659d4038625a7bbe762a75fb3303f828fa937634ec0a0fa600059f78c6bebc78d6c9dd49f2d653fe758a73d827560",
                //SHA512
                "03860a0f4bc14df02e0c939de21dfd68e4471fb42daaf248043278d55673fc7de33e171a56bc2dcf53062e018145b70c34be253a02509ce6400d1466df89b797"
            },
            //Hebrew: "שלום עולם" hashes
            {
                //MD5
                "60d3a0ea6eab05d5af5ba35c830a47b6",
                //SHA1
                "643cbc0fbf2800d790f329a1eb9cd544a2051e7b",
                //SHA256
                "5fdd1afb5cef74cd5b28d3e00fd12a2b638cefdc0642b99eb82822924713faca",
                //SHA384
                "edfe99b016dc3f5ab172bc5f75a363ad2d956c823566fa402f1d723a0619a2a58830d7824f8864965d47b19a43cc2378",
                //SHA512
                "af040fe4217cbcb2da8c3e717a8199714482b16f33638a2296e316ff5c3262e437b72023d06c9ecacba8e48a04651fdf3a09ed4be7546d71e397c6b6adbeddb1"
            },
            //Hindi: "नमस्ते दुनिया" hashes
            {
                //MD5
                "2e4a852c1b549f3f38a20f40b8956752",
                //SHA1
                "94397595c3440a3a260423f10ed089a258ed0721",
                //SHA256
                "d7be5996fa562294908ffe50c6aece46c34d6bed34e241dec5d1e8b5e83f7596",
                //SHA384
                "eb35244ff6268a937e1cf4029c6a86c4a05f739abe73ef88619c9b413846951c41a20fcdf79907d536f2e9567456a6e3",
                //SHA512
                "d758b849dabcf705c0876afe8356705701e7e8e985db704f30bb27ab195992fb748bb87181fcacd93c88d3d1014858d0144ea24accec609c931d785e1e0f9cf8"
            },
            //Japanese: "こんにちは世界" hashes
            {
                //MD5
                "9d735278cfbdb946834416adfb5aaf6c",
                //SHA1
                "a4d9dd44b0951a008fa84865df14d5b6c6f7ecdb",
                //SHA256
                "c6a304536826fb57e1b1896fcd8c91693a746233ae6a286dc85a65c8ae1f416f",
                //SHA384
                "c1c1a6214a2c7f2050bb93a8a7fdde6a369bae96a166a3ed6c1fd25ea9d8339bca528f140472bf3c803b0e77dab9dd72",
                //SHA512
                "573b794728cfb7ef4ccc12fd79237958ee7bc2ab5afbe2c9321ea9a34cbc66a1871b013030f8753c64051a8bd1fa8339fb51f10e466c1121d9f4dc7983507f31"
            },
            //Korean: "여보세요 세계" hashes
            {
                //MD5
                "b69cc2c3b49d0f44d5cdfb0a06116904",
                //SHA1
                "e7b6e55635b6efeedfe531330ebf949dda02758d",
                //SHA256
                "efb1f97168b707750b3b09ca891ad7177e763ffae1314117de1d2fabe46cecd8",
                //SHA384
                "805a270592ff98b52858bf4f40d58ace65ad5ad2a69701a19a584a4118e8f8547926f2d6322d469cc51295a2d8f18ea2",
                //SHA512
                "ed97c2f9da747f1059a6e4f71bc2080e7c9268eb0304ff51a8dfe9f4d35d3efd3367494045e711f83b3302cf9432d5c8780c2857913e0623b0497faa6e4a21b9"
            },
            //Portuguese: "Olá mundo" hashes
            {
                //MD5
                "622573cfe5b2ea9a8ce8cc5570bb0407",
                //SHA1
                "f6c2fc0dd7f1131d8cb5ac7420d77a4c28ac1aa0",
                //SHA256
                "44dae5dccc2e6ca3710dc84afd296754c6cd84465452d7a1d75d876528cc7e44",
                //SHA384
                "5eee18a6506a94e792dc0cef4bfbfbf5e463177ceecf82401f510b5f22e4d6a6e4935cc2269e2983ca9c9e196d9c88e7",
                //SHA512
                "1124add078f585a58ac85d32065ed8b6b18a7feace18892477353a43a29b3513c239f58d5eb2217b9358548034870fc026f938be65ebd8eec81e01508d50ee48"
            },
            //Russian: "Здравствуй, мир" hashes
            {
                //MD5
                "efeff92486727f0f4cc8a78c4da8d014",
                //SHA1
                "d38eb8d61ab65ee87eaa84460e833969bd89a0a6",
                //SHA256
                "ea69a929d34a1bd3eaf56e504dea127c3168946c8f562cea4333d250d327c082",
                //SHA384
                "2febd24501acfa4cc461587e6322db623322483d4fe7651993b68c1b689915b073bd88ec007bbdb4011cc6e75dae11de",
                //SHA512
                "da5b3b832f7e57abc8b48725c2f3a520e245e76f7bcd8b044ac8e68b13368e54f65e7d99af4567bd9d13d9853bc58c8a1e9dca6ff9c97fc78d7528822222f0fc"
            }
        };

        public IEnumerator<object[]> GetEnumerator()
        {
            for(var i = 0; i < inputsToHash.Length; i++)
            {
                for(var j = 0; j < hashAlgorithms.Length; j++)
                {
                    yield return new object[] { inputsToHash[i], hashAlgorithms[j], defaultEncoding, hashMatrix[i,j] };
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}