using System.Runtime.Versioning;
using System.Security.Cryptography;
using System.Text;

namespace AndreasReitberger.Shared.Core.Utilities
{
    /// <summary>
    /// Some parts of the code below base on the following source.
    /// Source #1: https://stackoverflow.com/a/22921355/10083577
    /// Author: https://stackoverflow.com/users/2270839/kevin
    /// License: https://creativecommons.org/licenses/by-sa/3.0/
    /// Modified: Yes
    /// ===========================================================
    /// Source #2: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.aes?view=net-8.0
    /// </summary>
    public partial class EncryptionManager
    {
        #region Methods
        /// <summary>
        /// Generates a random key (64 bit based) (#1)
        /// </summary>
        /// <returns>A 64 bit based <c>string</c></returns>
        public static string GenerateBase64Key()
        {
            byte[] key;
            string base64Key;
            using (Aes aes = Aes.Create())
            {
                // key as byte[]
                key = aes.Key;
                // key as base64string - which one you use depends on how you store your keys
                base64Key = Convert.ToBase64String(aes.Key);
            }
            return base64Key;
        }

        /// <summary>
        /// Converts a 64 bit based <c>string</c> to a <c>byte[] (#1)</c>
        /// </summary>
        /// <param name="sKey">Key to be converted</param>
        /// <returns><c>byte[]</c></returns>
        public static byte[] GetBytesFromBase64Key(string sKey) => Convert.FromBase64String(sKey);

        public static string GetStringFromBase64String(byte[] bytes) => Convert.ToBase64String(bytes);

        /// <summary>
        /// Encrypts a string with the passed key and returns a hased (encrypted) string. (#1 + #2)
        /// </summary>
        /// <param name="plainText">The text to be encrypted</param>
        /// <param name="key">The key for encryption</param>
        /// <returns>Encrypted <c>string</c></returns>
        public static string EncryptStringToBase64String(string plainText, string key, int keySize = 256)
            => EncryptStringToBase64String(plainText, GetBytesFromBase64Key(key), keySize);
        /// <summary>
        /// Encrypts a string with the passed key and returns a hased (encrypted) string. (#1 + #2)
        /// </summary>
        /// <param name="plainText">The text to be encrypted</param>
        /// <param name="key">The key for encryption</param>
        /// <returns>Encrypted <c>string</c></returns>
        public static string EncryptStringToBase64String(string plainText, byte[] key, int keySize = 256)
        {
            // Check arguments. 
            if (key == null || key.Length <= 0)
                throw new ArgumentNullException(nameof(key));
            byte[] returnValue;
            using (Aes aes = Aes.Create())
            {
                aes.KeySize = keySize;
                aes.GenerateIV();
                aes.Mode = CipherMode.CBC;
                byte[] iv = aes.IV;
                if (string.IsNullOrEmpty(plainText))
                    return Convert.ToBase64String(iv);
                ICryptoTransform encryptor = aes.CreateEncryptor(key, iv);

                // Create the streams used for encryption. 
                using MemoryStream msEncrypt = new();
                using CryptoStream csEncrypt = new(msEncrypt, encryptor, CryptoStreamMode.Write);
                using (StreamWriter swEncrypt = new(csEncrypt))
                {
                    //Write all data to the stream.
                    swEncrypt.Write(plainText);
                }
                // this is just our encrypted data
                byte[] encrypted = msEncrypt.ToArray();
                returnValue = new byte[encrypted.Length + iv.Length];
                // append our IV so our decrypt can get it
                Array.Copy(iv, returnValue, iv.Length);
                // append our encrypted data
                Array.Copy(encrypted, 0, returnValue, iv.Length, encrypted.Length);
            }
            // return encrypted bytes converted to Base64String
            return Convert.ToBase64String(returnValue);
        }

        public static string EncryptStreamToBase64String(MemoryStream stream, byte[] key, int keySize = 256)
            => EncryptStringToBase64String(GetStringFromBase64String(stream.ToArray()), key, keySize);

        /// <summary>
        /// Decrypts a string with the passed key and returns a plain text string. (#1 + #2)
        /// </summary>
        /// <param name="cipherText">Encrypted string</param>
        /// <param name="key">The key for encryption</param>
        /// <returns>Plain text <c>string</c></returns>
        public static string DecryptStringFromBase64String(string? cipherText, string key, int keySize = 256)
            => DecryptStringFromBase64String(cipherText, GetBytesFromBase64Key(key), keySize);

        /// <summary>
        /// Decrypts a string with the passed key and returns a plain text string. (#1 + #2)
        /// </summary>
        /// <param name="cipherText">Encrypted string</param>
        /// <param name="key">The key for encryption</param>
        /// <returns>Plain text <c>string</c></returns>
        public static string DecryptStringFromBase64String(string? cipherText, byte[] key, int keySize = 256)
        {
            // Check arguments. 
            if (string.IsNullOrEmpty(cipherText))
                return string.Empty;
            if (key == null || key.Length <= 0)
                throw new ArgumentNullException(nameof(key));

            string? plaintext = null;
            // this is all of the bytes
            byte[] allBytes = Convert.FromBase64String(cipherText);
            using (Aes aes = Aes.Create())
            {
                aes.KeySize = keySize;
                aes.Mode = CipherMode.CBC;

                // get our IV that we pre-pended to the data
                byte[] iv = new byte[aes.BlockSize / 8];
                if (allBytes.Length < iv.Length)
                    throw new ArgumentException("Message was less than IV size.");
                Array.Copy(allBytes, iv, iv.Length);
                // get the data we need to decrypt
                byte[] cipherBytes = new byte[allBytes.Length - iv.Length];
                Array.Copy(allBytes, iv.Length, cipherBytes, 0, cipherBytes.Length);

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = aes.CreateDecryptor(key, iv);

                // Create the streams used for decryption. 
                using MemoryStream msDecrypt = new(cipherBytes);
                using CryptoStream csDecrypt = new(msDecrypt, decryptor, CryptoStreamMode.Read);
                using StreamReader srDecrypt = new(csDecrypt);
                // Read the decrypted bytes from the decrypting stream 
                // and place them in a string.
                plaintext = srDecrypt.ReadToEnd();
            }
            return plaintext;
        }
        public static string DecryptStreamFromBase64String(MemoryStream stream, byte[] key, int keySize = 256)
            => DecryptStringFromBase64String(GetStringFromBase64String(stream.ToArray()), key, keySize);

        /// <summary>
        /// This method salts a user password with the provided salt. Make sure to use the same salt for decrypting
        /// Based on: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.passwordderivebytes?view=net-8.0
        /// </summary>
        /// <param name="password">The user password used for creating the key</param>
        /// <returns></returns>
        //[SupportedOSPlatform("Windows")]
#if NET6_0_OR_GREATER
        public static byte[] SaltWithPasswordString(string password, byte[]? salt = null, int keySize = 32, int iterations = 1000)
        {
            byte[] pwd = Encoding.Unicode.GetBytes(password);
            salt ??= CreateRandomSalt(7);
            try
            {      
                return Rfc2898DeriveBytes.Pbkdf2(pwd, salt, iterations, HashAlgorithmName.SHA256, keySize);
            }
            catch(Exception) { }
            finally
            {
                // Clear the buffers
                ClearBytes(pwd);
                ClearBytes(salt);
            }
            return [];
        }

        /// <summary>
        /// Generates a random salt
        /// </summary>
        /// <param name="length">Size of the array</param>
        /// <returns><c>byte[]</c> with random bytes</returns>
        public static byte[] CreateRandomSalt(int length) => RandomNumberGenerator.GetBytes(length >= 1 ? length : 1);

        /// <summary>
        /// Creates a hex string from the provided byte[].
        /// </summary>
        /// <param name="salt">The array to be converted</param>
        /// <returns>hex string</returns>
        public static string GetHexStringFromSalt(byte[] salt) => BitConverter.ToString(salt).Replace("-", "");

        /// <summary>
        /// Returns a byte[] from a hex string salt
        /// Based on: https://github.dev/tfsaggregator/aggregator-cli/blob/95a12bc65be1bca01bd585018b9fefb15d74457e/src/aggregator-shared/SharedSecret.cs#L12#L20
        /// </summary>
        /// <param name="saltHex"></param>
        /// <returns></returns>
        public static byte[] GetSaltFromHexString(string saltHex) => Enumerable.Range(0, saltHex.Length / 2).Select(x => Convert.ToByte(saltHex.Substring(x * 2, 2), 16)).ToArray();
#endif

        /// <summary>
        /// Clears the buffer
        /// Source: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.passwordderivebytes?view=net-8.0
        /// </summary>
        /// <param name="buffer"></param>
        /// <exception cref="ArgumentException"></exception>
        public static void ClearBytes(byte[] buffer)
        {
            // Check arguments.
            if (buffer == null)
            {
                throw new ArgumentException(nameof(buffer));
            }
            // Set each byte in the buffer to 0.
            for (int x = 0; x < buffer.Length; x++)
            {
                buffer[x] = 0;
            }
        }
        #endregion
    }
}
