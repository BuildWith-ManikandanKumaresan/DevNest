namespace DevNest.Manager.FileSystem
{
    public interface IFileSystemManager
    {
        string? RootDirectory { get; }
        string? ConfigurationDirectory { get; }
        string? PluginDirectory { get; }
        string? DataDirectrory { get; }
    }
}
