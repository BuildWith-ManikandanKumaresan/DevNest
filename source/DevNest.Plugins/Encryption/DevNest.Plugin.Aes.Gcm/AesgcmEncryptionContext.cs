#region using directives
using DevNest.Common.Base.Constants;
using DevNest.Common.Logger;
using DevNest.Plugin.Contracts.Encryption;
using System.Security.Cryptography;
using System.Text;
#endregion using directives

namespace DevNest.Plugin.Aes.Gcm
{
    /// <summary>
    /// Represents the class instance for AES-GCM encryption data context plugin.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="_connectionParams"></param>
    public class AesgcmEncryptionContext<T>(Dictionary<string, object>? _connectionParams, IAppLogger<AesgcmEncryptionPlugin> logger) : ICryptoContext<T> where T : class
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        private readonly string _key = _connectionParams.TryGetValue(ConnectionParamConstants.EncryptionKey, out var key) && key is string keyString ? keyString : throw new ArgumentNullException(nameof(_connectionParams), "Encryption key is required.");
        private readonly IAppLogger<AesgcmEncryptionPlugin> _logger = logger;
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        /// <summary>
        /// Gets the connection parameters for the data context.
        /// </summary>
        public Dictionary<string, object>? ConnectionParams { get; private set; } = _connectionParams;

        /// <summary>
        /// Decrypts the given cipher text using the specified key.
        /// </summary>
        /// <param name="cipherText"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public T? Decrypt(T cipherText)
        {
            _logger.LogDebug($"{nameof(AesgcmEncryptionContext<T>)} => {nameof(Decrypt)} called with cipherText: {cipherText}");
            var data = Convert.FromBase64String(cipherText as string ?? string.Empty);
            var nonce = data[..12];
            var tag = data[^16..];
            var cipherBytes = data[12..^16];
            var plainBytes = new byte[cipherBytes.Length];

#pragma warning disable SYSLIB0053 // Type or member is obsolete
            using (var aesGcm = new AesGcm(SHA256.HashData(Encoding.UTF8.GetBytes(_key))))
            {
                aesGcm.Decrypt(nonce, cipherBytes, tag, plainBytes);
                _logger.LogDebug($"{nameof(AesgcmEncryptionContext<T>)} => {nameof(Decrypt)} completed successfully.");
            }
#pragma warning restore SYSLIB0053 // Type or member is obsolete
            return Encoding.UTF8.GetString(plainBytes) as T;
        }

        /// <summary>
        /// Encrypts the given plain text using the specified key.
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public T? Encrypt(T plainText)
        {
            _logger.LogDebug($"{nameof(AesgcmEncryptionContext<T>)} => {nameof(Encrypt)} called with plainText: {plainText}");
            var keyBytes = SHA256.HashData(Encoding.UTF8.GetBytes(_key));
            var nonce = RandomNumberGenerator.GetBytes(12);
            var plainBytes = Encoding.UTF8.GetBytes(plainText as string ?? string.Empty);
            var cipherBytes = new byte[plainBytes.Length];
            var tag = new byte[16];

#pragma warning disable SYSLIB0053 // Type or member is obsolete
            using (var aesGcm = new AesGcm(keyBytes))
            {
                aesGcm.Encrypt(nonce, plainBytes, cipherBytes, tag);
                _logger.LogDebug($"{nameof(AesgcmEncryptionContext<T>)} => {nameof(Encrypt)} completed successfully.");
            }
#pragma warning restore SYSLIB0053 // Type or member is obsolete
            return Convert.ToBase64String(nonce.Concat(cipherBytes).Concat(tag).ToArray()) as T;
        }
    }
}