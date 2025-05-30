namespace DevNest.Common.Base.Entity
{
    /// <summary>
    /// Represents the class instance for plugin manifest collection.
    /// </summary>
    public class PluginManifestCollection
    {
        public IEnumerable<PluginManifest>? Plugins { get; set; }
    }
}