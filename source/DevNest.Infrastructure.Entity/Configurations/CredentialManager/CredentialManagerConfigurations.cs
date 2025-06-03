namespace DevNest.Infrastructure.Entity.Configurations.CredentialManager
{
    /// <summary>
    /// Represents the configuration for credential manager storage.
    /// </summary>
    public class CredentialManagerConfigurations
    {
        public IEnumerable<Dictionary<string, object>> Storage { get; set; }
    }
}
