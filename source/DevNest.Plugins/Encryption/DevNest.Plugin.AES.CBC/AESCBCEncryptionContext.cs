#region using directives
using DevNest.Common.Base.Constants;
using DevNest.Common.Logger;
using DevNest.Plugin.Contracts.Encryption;
using System.Security.Cryptography;
using System.Text;
#endregion using directives

namespace DevNest.Plugin.AES.CBC
{
    /// <summary>
    /// Represents the class instance for AES-CBC encryption data context plugin.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <remarks>
    /// Initializes a new instance of the <see cref="AescbcEncryptionContext{T}"/> class with the specified connection parameters.
    /// </remarks>
    /// <param name="_connectionParams"></param>
    public class AescbcEncryptionContext<T>(Dictionary<string, object>? _connectionParams, IAppLogger<AescbcEncryptionPlugin> logger) : IEncryptionContext<T> where T : class
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        private readonly string _key = _connectionParams.TryGetValue(ConnectionParamConstants.EncryptionKey, out var key) && key is string keyString ? keyString : throw new ArgumentNullException(nameof(_connectionParams), "Encryption key is required.");
        private readonly IAppLogger<AescbcEncryptionPlugin> _logger = logger ?? throw new ArgumentNullException(nameof(logger), "Logger cannot be null.");
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        /// <summary>
        /// Gets the connection parameters for the data context.
        /// </summary>
        public Dictionary<string, object>? ConnectionParams { get; private set; } = _connectionParams;

        /// <summary>
        /// Encrypts the given plain text using the specified key.
        /// </summary>
        /// <param name="cipherText"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public T? Decrypt(T cipherText)
        {
            _logger.LogDebug($"{nameof(AescbcEncryptionContext<T>)} => Decrypting cipher text.", new { CipherText = cipherText });
            var fullData = Convert.FromBase64String(cipherText as string ?? string.Empty);
            byte[] iv = fullData[..16];
            byte[] cipherBytes = fullData[16..];

            using var aes = Aes.Create();
            aes.Key = SHA256.HashData(Encoding.UTF8.GetBytes(_key));
            aes.IV = iv;

            using var decryptor = aes.CreateDecryptor();
            var decryptedBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
            _logger.LogDebug($"{nameof(AescbcEncryptionContext<T>)} => Decryption successful.", new { CipherText = cipherText });
            return Encoding.UTF8.GetString(decryptedBytes) as T;
        }

        /// <summary>
        /// Encrypts the given plain text using the specified key.
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public T? Encrypt(T plainText)
        {
            _logger.LogDebug($"{nameof(AescbcEncryptionContext<T>)} => Encrypting plain text.", new { PlainText = plainText });
            using var aes = Aes.Create();
            aes.Key = SHA256.HashData(Encoding.UTF8.GetBytes(_key));
            aes.GenerateIV();

            using var encryptor = aes.CreateEncryptor();
            var inputBytes = Encoding.UTF8.GetBytes(plainText as string ?? string.Empty);
            var encryptedBytes = encryptor.TransformFinalBlock(inputBytes, 0, inputBytes.Length);
            _logger.LogDebug($"{nameof(AescbcEncryptionContext<T>)} => Encryption successful.", new { PlainText = plainText });
            return Convert.ToBase64String(aes.IV.Concat(encryptedBytes).ToArray()) as T;
        }
    }
}