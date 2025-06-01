using System.Reflection;

namespace DevNest.Manager.FileSystem
{
    /// <summary>
    /// Represents the class interface for File system manager.
    /// </summary>
    public class FileSystemManager : IFileSystemManager
    {
        /// <summary>
        /// Gets or sets the filesystem root directory.
        /// </summary>
        public string? RootDirectory => GetRootDirectory();

        /// <summary>
        /// Get configuration directory from the file systems.
        /// </summary>
        public string? ConfigurationDirectory => GetConfigurationDirectory();

        /// <summary>
        /// Get the directry path than contains the plugins.
        /// </summary>
        public string? PluginDirectory => GetPluginDirectory();

        /// <summary>
        /// Get the directory path that contains the data's.
        /// </summary>
        public string? DataDirectrory => GetDataDirectory();

        /// <summary>
        /// Handler method to Get the root directory.
        /// </summary>
        /// <returns></returns>
        private string? GetRootDirectory()
        {
            string executionPath = Path.GetFullPath(Directory.GetParent(Assembly.GetExecutingAssembly().Location)?.FullName ?? string.Empty);
            if (!Directory.Exists(executionPath))
                return null;
            return executionPath;
        }

        /// <summary>
        /// Handler method to get the directory that contains the configuration files.
        /// </summary>
        /// <returns></returns>
        private string? GetConfigurationDirectory()
        {
            string configDir = Path.GetFullPath(Path.Combine(GetRootDirectory() ?? string.Empty, "..", "configurations"));
            if (!Directory.Exists(configDir))
                return null;
            return configDir;
        }

        /// <summary>
        /// Handler method to get the directory path that contains the plugins.
        /// </summary>
        /// <returns></returns>
        private string? GetPluginDirectory()
        {
            string plugInDirectory = Path.GetFullPath(Path.Combine(GetRootDirectory() ?? string.Empty, "Plugins"));
            if (!Directory.Exists(plugInDirectory))
                return null;
            return plugInDirectory;
        }

        /// <summary>
        /// Handler method to get the directory path that contains the data.
        /// </summary>
        /// <returns></returns>
        private string? GetDataDirectory()
        {
            string configDir = Path.GetFullPath(Path.Combine(GetRootDirectory() ?? string.Empty, "..", "data"));
            if (!Directory.Exists(configDir))
                return null;
            return configDir;
        }



    }
}
