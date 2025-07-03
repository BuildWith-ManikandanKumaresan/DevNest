#region using directives
using DevNest.Plugin.Contracts.Base;
using DevNest.Plugin.Contracts.Context;
#endregion using directives

namespace DevNest.Plugin.Contracts.Plugin
{
    /// <summary>
    /// Represents the interface instances of encryption plugins must implement.
    /// </summary>
    public interface IEncryptionPlugin : IPluginBase
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