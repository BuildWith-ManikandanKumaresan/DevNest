namespace DevNest.Plugin.Contracts.Encryption
{
    /// <summary>
    /// Represents the interface for data context operations for encryption plugins.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICryptoContext<T> where T : class
    {
        /// <summary>
        /// Encrypts the given plain text using the specified key.
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        T? Encrypt(T plainText);

        /// <summary>
        /// Decrypts the given cipher text using the specified key.
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        T? Decrypt(T cipherText);
    }
}