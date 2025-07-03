#region using directives
using DevNest.Common.Manager.Plugin;
#endregion using directives

namespace DevNest.VaultX.Api
{
    /// <summary>
    /// Initializes plugins at application startup.
    /// </summary>
    public class PluginInitializer(IPluginManager pluginManager) : IHostedService
    {
        private readonly IPluginManager _pluginManager = pluginManager;

        /// <summary>
        /// Starts the plugin manager to load encryption and store plugins.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _pluginManager.LoadEncryptionPlugins();
            _pluginManager.LoadStorePlugins();
            return Task.CompletedTask;
        }

        /// <summary>
        /// Stops the plugin manager gracefully when the application is shutting down.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
