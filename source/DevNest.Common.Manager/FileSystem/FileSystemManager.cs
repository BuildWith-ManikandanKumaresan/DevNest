#region using directives
using DevNest.Common.Base.Constants;
using System.Reflection;
#endregion using directives

namespace DevNest.Common.Manager.FileSystem
{
    /// <summary>
    /// Represents the class interface for managing file systems.
    /// </summary>
    public class FileSystemManager : IFileSystemManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileSystemManager"/> class.
        /// </summary>
        public FileSystemManager()
        {
            Plugin = new FileSystem(PluginDirectory ?? string.Empty, FileSearchPatternConstants.DevNest_Plugins);
            Logger = new FileSystem(LoggerDirectory ?? string.Empty, FileSearchPatternConstants.DevNest_Logs);
            Preferences = new FileSystem(PreferencesDirectory ?? string.Empty, FileSearchPatternConstants.Extension_Preferences);
            Resources = new FileSystem(ResourcesDirectory ?? string.Empty, FileSearchPatternConstants.Extension_Resources);
            SecureVault = new FileSystem(SecureVaultDirectory ?? string.Empty, FileSearchPatternConstants.Extension_Data);
        }

        /// <summary>
        /// Gets the root directory of the file system.
        /// </summary>
        public IFileSystem? Plugin { get; set; }

        /// <summary>
        /// Gets the directory that contains the logger files.
        /// </summary>
        public IFileSystem? Logger { get; set; }

        /// <summary>
        /// Gets the directory that contains the preferences files.
        /// </summary>
        public IFileSystem? Preferences { get; set; }

        /// <summary>
        /// Gets the directory that contains the resources files.
        /// </summary>
        public IFileSystem? Resources { get; set; }

        /// <summary>
        /// Gets the directory that contains the secure vault files for storing sensitive data.
        /// </summary>
        public IFileSystem? SecureVault { get; set; }

        #region Private methods

        /// <summary>
        /// Handler method to Get the root directory.
        /// </summary>
        /// <returns></returns>
        private static string? RootDirectory
        {
            get
            {
                string executionPath = Path.GetFullPath(Directory.GetParent(Assembly.GetExecutingAssembly().Location)?.FullName ?? string.Empty);
                return executionPath != null && Directory.Exists(executionPath) ? Path.GetFullPath(executionPath) : null;
            }
        }

        /// <summary>
        /// Handler method to get the directory path that contains the plugins.
        /// </summary>
        /// <returns></returns>
        private static string? PluginDirectory
        {
            get
            {
                string plugInDirectory = Path.GetFullPath(Path.Combine(RootDirectory ?? string.Empty, FileSystemConstants.PluginDirectory));
                if (!Directory.Exists(plugInDirectory))
                    Directory.CreateDirectory(plugInDirectory);
                return plugInDirectory;
            }
        }

        /// <summary>
        /// Handler method to get the directory path that contains the encryption plugins.
        /// </summary>
        /// <returns></returns>
        private static string? LoggerDirectory
        {
            get
            {
                string loggerDir = Path.GetFullPath(Path.Combine(RootDirectory ?? string.Empty,
                    FileSystemConstants.DirectoryUp,
                    FileSystemConstants.DevNestDirectory,
                    FileSystemConstants.LoggerDirectory));
                if (!Directory.Exists(loggerDir))
                    Directory.CreateDirectory(loggerDir);
                return loggerDir;
            }
        }

        /// <summary>
        /// Handler method to get the directory that contains the configuration files.
        /// </summary>
        /// <returns></returns>
        private static string? PreferencesDirectory
        {
            get
            {
                string configDir = Path.GetFullPath(Path.Combine(RootDirectory ?? string.Empty,
                    FileSystemConstants.DirectoryUp,
                    FileSystemConstants.DevNestDirectory,
                    FileSystemConstants.PreferencesDirectory));
                if (!Directory.Exists(configDir))
                    Directory.CreateDirectory(configDir);
                return configDir;
            }
        }

        /// <summary>
        /// Handler method to get the directory path that contains the assets.
        /// </summary>
        /// <returns></returns>
        private static string? ResourcesDirectory
        {
            get
            {
                string assetsDir = Path.GetFullPath(Path.Combine(RootDirectory ?? string.Empty,
                    FileSystemConstants.DirectoryUp,
                    FileSystemConstants.DevNestDirectory,
                    FileSystemConstants.ResourcesDirectory));
                if (!Directory.Exists(assetsDir))
                    Directory.CreateDirectory(assetsDir);
                return assetsDir;
            }
        }

        /// <summary>
        /// Handler method to get the directory path that contains the data.
        /// </summary>
        /// <returns></returns>
        private static string? SecureVaultDirectory
        {
            get
            {
                string credentialDataDir = Path.GetFullPath(Path.Combine(RootDirectory ?? string.Empty,
                    FileSystemConstants.DirectoryUp,
                    FileSystemConstants.DevNestDirectory,
                    FileSystemConstants.SecureVaultDirectory));
                if (!Directory.Exists(credentialDataDir))
                    Directory.CreateDirectory(credentialDataDir);
                return credentialDataDir;
            }
        }

        #endregion Private methods
    }
}