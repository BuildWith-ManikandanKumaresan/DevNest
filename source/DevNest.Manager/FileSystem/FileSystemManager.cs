using System.Reflection;

namespace DevNest.Manager.FileSystem
{
    /// <summary>
    /// Represents the class interface for File system manager.
    /// </summary>
    public class FileSystemManager : IFileSystemManager
    {
        public string? RootDirectory => GetRootDirectory();

        public string? ConfigurationDirectory => GetConfigurationDirectory();

        public string? PluginDirectory => GetPluginDirectory();

        public string? DataDirectrory => GetDataDirectory();

        private string? GetRootDirectory()
        {
            string executionPath = Path.GetFullPath(Directory.GetParent(Assembly.GetExecutingAssembly().Location)?.FullName ?? string.Empty);
            if (!Directory.Exists(executionPath))
                return null;
            return executionPath;
        }

        private string? GetConfigurationDirectory()
        {
            string configDir = Path.GetFullPath(Path.Combine(GetRootDirectory() ?? string.Empty, "..", "configurations"));
            if (!Directory.Exists(configDir))
                return null;
            return configDir;
        }

        private string? GetPluginDirectory()
        {
            string plugInDirectory = Path.GetFullPath(Path.Combine(GetRootDirectory() ?? string.Empty, "Plugins"));
            if (!Directory.Exists(plugInDirectory))
                return null;
            return plugInDirectory;
        }

        private string? GetDataDirectory()
        {
            string configDir = Path.GetFullPath(Path.Combine(GetRootDirectory() ?? string.Empty, "..", "data"));
            if (!Directory.Exists(configDir))
                return null;
            return configDir;
        }



    }
}
