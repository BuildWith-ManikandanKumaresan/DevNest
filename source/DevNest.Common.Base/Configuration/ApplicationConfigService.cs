#region use directive
using DevNest.Common.Base.Contracts;
using Microsoft.Extensions.Options;
#endregion use directive

/// <summary>
/// Represents the class structure for standard configuration of type T.
/// </summary>
/// <typeparam name="T"></typeparam>
public class ApplicationConfigService<T> : IApplicationConfigService<T> where T: class, new()
{
    private readonly IOptionsMonitor<T> _optionsMonitor;

    /// <summary>
    /// Intialize the constructor for configurations services of type T.
    /// </summary>
    /// <param name="optionsMonitor"></param>
    public ApplicationConfigService(IOptionsMonitor<T> optionsMonitor)
    {
        _optionsMonitor = optionsMonitor;
    }

    /// <summary>
    /// Gets the configuration of type T.
    /// </summary>
    public T? Value => _optionsMonitor.CurrentValue;
}