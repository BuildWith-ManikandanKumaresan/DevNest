#region using directives
using DevNest.Common.Base.Contracts;
using Microsoft.Extensions.Options;
#endregion using directives

namespace DevNest.Common.Base.Configuration
{
    /// <summary>
    /// Represents the class structure for standard configuration of type T.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <remarks>
    /// Intialize the constructor for configurations services of type T.
    /// </remarks>
    /// <param name="optionsMonitor"></param>
    public class AppConfigService<T>(IOptionsMonitor<T> optionsMonitor) : IAppConfigService<T> where T : class, new()
    {
        private readonly IOptionsMonitor<T> _optionsMonitor = optionsMonitor;

        /// <summary>
        /// Gets the configuration of type T.
        /// </summary>
        public T? Value => _optionsMonitor.CurrentValue;
    }
}