#region using directives
using DevNest.Common.Base.Constants;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Text;
using DevNest.Common.Logger;
using DevNest.Plugin.Contracts.Context;
#endregion using directives

namespace DevNest.Encryption.Plugin.RSA
{
    /// <summary>
    /// Represents the class instance for RSA encryption data context plugin.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="_connectionParams"></param>
    public class EncryptionContext<T> : IEncryptionContext<T> where T : class
    {
        private readonly string? _privateKey;
        private readonly string? _publicKey;
        private readonly IAppLogger<EncryptionPlugin> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="EncryptionContext{T}"/> class with the specified connection parameters.
        /// </summary>
        /// <param name="_connectionParams"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public EncryptionContext(Dictionary<string, object>? _connectionParams, IAppLogger<EncryptionPlugin> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), "Logger cannot be null.");
            ConnectionParams = _connectionParams;
            (_publicKey, _privateKey) = GenerateKeys();
        }

        /// <summary>
        /// Gets the connection parameters for the data context.
        /// </summary>
        public Dictionary<string, object>? ConnectionParams { get; private set; }

        /// <summary>
        /// Decrypts the given cipher text using the specified key.
        /// </summary>
        /// <param name="cipherText"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public T? Decrypt(T cipherText)
        {
            try
            {
                _logger.LogDebug($"{nameof(EncryptionContext<T>)} => Decrypting cipher text.", new { CipherText = cipherText });
                using var rsa = System.Security.Cryptography.RSA.Create();
                rsa.ImportRSAPrivateKey(Convert.FromBase64String(_privateKey ?? string.Empty), out _);
                byte[] decryptedBytes = rsa.Decrypt(Convert.FromBase64String(cipherText as string ?? string.Empty), RSAEncryptionPadding.OaepSHA256);
                _logger.LogDebug($"{nameof(EncryptionContext<T>)} => Decryption successful.", new { CipherText = cipherText });
                string encodedString = Encoding.UTF8.GetString(decryptedBytes);
                return encodedString as T;
            }
            catch (Exception)
            {
                _logger.LogError($"{nameof(EncryptionContext<T>)} => Decryption failed.", request: new { CipherText = cipherText });
            }
            return string.Empty as T;
        }

        /// <summary>
        /// Encrypts the given plain text using the specified key.
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public T? Encrypt(T plainText)
        {
            _logger.LogDebug($"{nameof(EncryptionContext<T>)} => Encrypting plain text.", new { PlainText = plainText });
            using var rsa = System.Security.Cryptography.RSA.Create();
            rsa.ImportRSAPublicKey(Convert.FromBase64String(_publicKey ?? string.Empty), out _);
            var encryptedBytes = rsa.Encrypt(Encoding.UTF8.GetBytes(plainText as string ?? string.Empty), RSAEncryptionPadding.OaepSHA256);
            _logger.LogDebug($"{nameof(EncryptionContext<T>)} => Encryption successful.", new { PlainText = plainText });
            return Convert.ToBase64String(encryptedBytes) as T;
        }

        /// <summary>
        /// Generates a new RSA key pair (public and private keys).
        /// </summary>
        /// <returns></returns>
        public (string PublicKey, string PrivateKey) GenerateKeys()
        {
            _logger.LogDebug($"{nameof(EncryptionContext<T>)} => Generating RSA key pair.");
            using var rsa = System.Security.Cryptography.RSA.Create();
            var pubKey = Convert.ToBase64String(rsa.ExportRSAPublicKey());
            var privKey = Convert.ToBase64String(rsa.ExportRSAPrivateKey());
            _logger.LogDebug($"{nameof(EncryptionContext<T>)} => RSA key pair generated successfully.", new { PublicKey = pubKey, PrivateKey = privKey });
            return (pubKey, privKey);
        }
    }
}