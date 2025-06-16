#region using directives
using DevNest.Common.Base.Constants;
using DevNest.Plugin.Contracts.Encryption;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Text;
using DevNest.Common.Logger;
#endregion using directives

namespace DevNest.Plugin.Rsa
{
    /// <summary>
    /// Represents the class instance for RSA encryption data context plugin.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="_connectionParams"></param>
    public class RsaEncryptionContext<T> : IEncryptionContext<T> where T : class
    {
        private readonly string? _privateKey;
        private readonly string? _publicKey;
        private readonly IAppLogger<RsaEncryptionPlugin> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="RsaEncryptionContext{T}"/> class with the specified connection parameters.
        /// </summary>
        /// <param name="_connectionParams"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public RsaEncryptionContext(Dictionary<string, object>? _connectionParams, IAppLogger<RsaEncryptionPlugin> logger)
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
        public T Decrypt(T cipherText)
        {
            try
            {
                _logger.LogDebug($"{nameof(RsaEncryptionContext<T>)} => Decrypting cipher text.", new { CipherText = cipherText });
                using var rsa = RSA.Create();
                rsa.ImportRSAPrivateKey(Convert.FromBase64String(_privateKey), out _);
                var decryptedBytes = rsa.Decrypt(Convert.FromBase64String(cipherText as string), RSAEncryptionPadding.OaepSHA256);
                _logger.LogDebug($"{nameof(RsaEncryptionContext<T>)} => Decryption successful.", new { CipherText = cipherText });
                return Encoding.UTF8.GetString(decryptedBytes) as T;
            }
            catch (Exception)
            {
                _logger.LogError($"{nameof(RsaEncryptionContext<T>)} => Decryption failed.", request: new { CipherText = cipherText });
            }
            return string.Empty as T;
        }

        /// <summary>
        /// Encrypts the given plain text using the specified key.
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public T Encrypt(T plainText)
        {
            _logger.LogDebug($"{nameof(RsaEncryptionContext<T>)} => Encrypting plain text.", new { PlainText = plainText });
            using var rsa = RSA.Create();
            rsa.ImportRSAPublicKey(Convert.FromBase64String(_publicKey), out _);
            var encryptedBytes = rsa.Encrypt(Encoding.UTF8.GetBytes(plainText as string), RSAEncryptionPadding.OaepSHA256);
            _logger.LogDebug($"{nameof(RsaEncryptionContext<T>)} => Encryption successful.", new { PlainText = plainText });
            return Convert.ToBase64String(encryptedBytes) as T;
        }

        /// <summary>
        /// Generates a new RSA key pair (public and private keys).
        /// </summary>
        /// <returns></returns>
        public (string PublicKey, string PrivateKey) GenerateKeys()
        {
            _logger.LogDebug($"{nameof(RsaEncryptionContext<T>)} => Generating RSA key pair.");
            using var rsa = RSA.Create();
            var pubKey = Convert.ToBase64String(rsa.ExportRSAPublicKey());
            var privKey = Convert.ToBase64String(rsa.ExportRSAPrivateKey());
            _logger.LogDebug($"{nameof(RsaEncryptionContext<T>)} => RSA key pair generated successfully.", new { PublicKey = pubKey, PrivateKey = privKey });
            return (pubKey, privKey);
        }
    }
}