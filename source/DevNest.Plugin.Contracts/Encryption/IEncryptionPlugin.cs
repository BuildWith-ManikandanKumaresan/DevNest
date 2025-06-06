﻿namespace DevNest.Plugin.Contracts.Encryption
{
    /// <summary>
    /// Represents the interface instances of encryption plugins must implement.
    /// </summary>
    public interface IEncryptionPlugin : IPlugin
    {
        /// <summary>
        /// Gets the encryption data context for the plugin with the specified connection parameters.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connectionParams"></param>
        /// <returns></returns>
        IEncryptionContext<T>? GetEncryptionContext<T>(Dictionary<string, object> connectionParams) where T : class;
    }
}