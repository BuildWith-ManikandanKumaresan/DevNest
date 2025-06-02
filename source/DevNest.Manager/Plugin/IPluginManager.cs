#region using directives
using DevNest.Plugin.Contracts;
#endregion using directives

namespace DevNest.Manager.Plugin
{
    /// <summary>
    /// Represents the interface for managing plugins in the application.
    /// </summary>
    public interface IPluginManager
    {
        /// <summary>
        /// Gets the data context for the plugin with the specified connection parameters.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connectionParams"></param>
        /// <returns></returns>
        IDataContext<T>? GetContext<T>(Dictionary<string, object> connectionParams) where T : class;
    }
}