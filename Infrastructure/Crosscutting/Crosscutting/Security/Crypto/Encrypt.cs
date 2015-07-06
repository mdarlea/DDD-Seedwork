using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace GTE.Common.Infrastructure.Core.Security.Crypto
{
    public static class Encrypt
    {
        public static string EncryptString(string plainText, byte[] key, byte[] iv)
        {
            byte[] returnBuffer;

            var symetricAlgorithm = Rijndael.Create();
            symetricAlgorithm.Key = key;
            symetricAlgorithm.IV = iv;

            using (var inMemory = new MemoryStream())
            {
                using (var encryptionStream = new CryptoStream(inMemory, symetricAlgorithm.CreateEncryptor(),CryptoStreamMode.Write))
                {
                    var dataBuffer = Encoding.ASCII.GetBytes(plainText);
                    encryptionStream.Write(dataBuffer, 0, dataBuffer.Length);

                    //encryptionStream.FlushFinalBlock();

                    encryptionStream.Close();
                    returnBuffer = inMemory.ToArray();
                }
            }

            // Convert the encrypted byte array to a base64 encoded string
            var cipherText = Convert.ToBase64String(returnBuffer, 0, returnBuffer.Length);

            // Return the encrypted data as a string
            return cipherText;
        }
    }
}
