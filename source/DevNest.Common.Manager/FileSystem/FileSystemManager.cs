#region using directives
using DevNest.Common.Base.Constants;
using System.Reflection;
#endregion using directives

namespace DevNest.Common.Manager.FileSystem
{
    /// <summary>
    /// Represents the class interface for File system manager.
    /// </summary>
    public class FileSystemManager : IFileSystemManager
    {
        private string? _rootDirectory = string.Empty;

        /// <summary>
        /// Gets or sets the filesystem root directory.
        /// </summary>
        public string? RootDirectory
        {
            get
            {
                if(string.IsNullOrEmpty(_rootDirectory))
                {
                    _rootDirectory = GetRootDirectory() ?? string.Empty;
                }
                return _rootDirectory;
            }
        }

        /// <summary>
        /// Get configuration directory from the file systems.
        /// </summary>
        public string? ConfigurationDirectory => GetConfigurationDirectory();

        /// <summary>
        /// Get the directry path than contains the plugins.
        /// </summary>
        public string? PluginStorageDirectory => GetPluginStorageDirectory();

        /// <summary>
        /// Get the directory path that contains the data's.
        /// </summary>
        public string? CredentialDataDirectrory => GetCredentialDataDirectory();

        /// <summary>
        /// Get the directory path that contains the assets.
        /// </summary>
        public string? AssetsDirectory => GetAssetsDirectory();

        /// <summary>
        /// Get the directory path that contains the error codes.
        /// </summary>
        public string? ErrorCodesDirectory => GetErrorCodesDirectory();

        /// <summary>
        /// Get the directory path that contains the warning codes.
        /// </summary>
        public string? WarningCodesDirectory => GetWarningCodesDirectory();

        /// <summary>
        /// Get the directory path that contains the success codes.
        /// </summary>
        public string? SuccessCodesDirectory => GetSuccessCodesDirectory();

        /// <summary>
        /// Get the directory path that contains the encryption plugin storage.
        /// </summary>
        public string? EncryptionPluginStorageDirectory => GetEncryptionPluginDirectory();

        /// <summary>
        /// Get the directory path that contains the workspace.
        /// </summary>
        /// <param name="workspaceName"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public string? GetWorkSpaceDirectory(string workspaceName)
        {
            if (string.IsNullOrEmpty(workspaceName))
                workspaceName = CommonConstants.DefaultWorkspace;
            string workspaceDirectory = Path.GetFullPath(Path.Combine(GetCredentialDataDirectory() ?? string.Empty, workspaceName));
            if (!Directory.Exists(workspaceDirectory))
                Directory.CreateDirectory(workspaceDirectory);
            return workspaceDirectory;
        }

        #region Private methods

        /// <summary>
        /// Handler method to Get the root directory.
        /// </summary>
        /// <returns></returns>
        private static string? GetRootDirectory()
        {
            try
            {
                string executionPath = Path.GetFullPath(Directory.GetParent(Assembly.GetExecutingAssembly().Location)?.FullName ?? string.Empty);
                return executionPath != null && Directory.Exists(executionPath) ? Path.GetFullPath(executionPath) : null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Handler method to get the directory that contains the configuration files.
        /// </summary>
        /// <returns></returns>
        private string? GetConfigurationDirectory()
        {
            string configDir = Path.GetFullPath(Path.Combine(RootDirectory ?? string.Empty, FileSystemConstants.FolderUp, FileSystemConstants.ConfigurationsDirectoryName));
            if (!Directory.Exists(configDir))
                Directory.CreateDirectory(configDir);
            return configDir;
        }

        /// <summary>
        /// Handler method to get the directory path that contains the plugins.
        /// </summary>
        /// <returns></returns>
        private string? GetPluginStorageDirectory()
        {
            string plugInDirectory = Path.GetFullPath(Path.Combine(RootDirectory ?? string.Empty, FileSystemConstants.StoragePluginsDirectoryName));
            if (!Directory.Exists(plugInDirectory))
                Directory.CreateDirectory(plugInDirectory);
            return plugInDirectory;
        }

        /// <summary>
        /// Handler method to get the directory path that contains the encryption plugins.
        /// </summary>
        /// <returns></returns>
        private string? GetEncryptionPluginDirectory()
        {
            string plugInDirectory = Path.GetFullPath(Path.Combine(RootDirectory ?? string.Empty, FileSystemConstants.EncryptionPluginsDirectoryName));
            if (!Directory.Exists(plugInDirectory))
                Directory.CreateDirectory(plugInDirectory);
            return plugInDirectory;
        }

        /// <summary>
        /// Handler method to get the directory path that contains the data.
        /// </summary>
        /// <returns></returns>
        private string? GetCredentialDataDirectory()
        {
            string credentialDataDir = Path.GetFullPath(Path.Combine(RootDirectory ?? string.Empty, FileSystemConstants.FolderUp, FileSystemConstants.DatasDirectoryName, FileSystemConstants.CredentialManagerDataDirectory));
            if (!Directory.Exists(credentialDataDir))
                Directory.CreateDirectory(credentialDataDir);
            return credentialDataDir;
        }

        /// <summary>
        /// Handler method to get the directory path that contains the assets.
        /// </summary>
        /// <returns></returns>
        private string? GetAssetsDirectory()
        {
            string assetsDir = Path.GetFullPath(Path.Combine(RootDirectory ?? string.Empty, FileSystemConstants.FolderUp, FileSystemConstants.AssetsDirectoryName));
            if (!Directory.Exists(assetsDir))
                Directory.CreateDirectory(assetsDir);
            return assetsDir;
        }

        /// <summary>
        /// Handler method to get the directory path that contains the assets.
        /// </summary>
        /// <returns></returns>
        private string? GetWarningCodesDirectory()
        {
            string warningCodesDir = Path.GetFullPath(Path.Combine(GetAssetsDirectory() ?? string.Empty, FileSystemConstants.WarningCodesDirectoryName));
            if (!Directory.Exists(warningCodesDir))
                Directory.CreateDirectory(warningCodesDir);
            return warningCodesDir;
        }

        /// <summary>
        /// Handler method to get the directory path that contains the error codes.
        /// </summary>
        /// <returns></returns>
        private string? GetErrorCodesDirectory()
        {
            string errorCodesDir = Path.GetFullPath(Path.Combine(GetAssetsDirectory() ?? string.Empty, FileSystemConstants.ErrorCodesDirectoryName));
            if (!Directory.Exists(errorCodesDir))
                Directory.CreateDirectory(errorCodesDir);
            return errorCodesDir;
        }

        /// <summary>
        /// Handler method to get the directory path that contains the success codes.
        /// </summary>
        /// <returns></returns>
        private string? GetSuccessCodesDirectory()
        {
            string successCodesDir = Path.GetFullPath(Path.Combine(GetAssetsDirectory() ?? string.Empty, FileSystemConstants.SuccessCodesDirectoryName));
            if (!Directory.Exists(successCodesDir))
                Directory.CreateDirectory(successCodesDir);
            return successCodesDir;
        }

        #endregion Private methods
    }
}
