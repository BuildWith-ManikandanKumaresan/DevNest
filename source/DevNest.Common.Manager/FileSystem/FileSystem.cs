#region using directives
using DevNest.Common.Base.Constants;
#endregion using directives

namespace DevNest.Common.Manager.FileSystem
{
    /// <summary>
    /// Represents a file system structure that allows navigation through directories and files.
    /// </summary>
    public class FileSystem : IFileSystem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileSystem"/> class with the specified root directory.
        /// </summary>
        /// <param name="root"></param>
        public FileSystem(string root)
        {
            this.Root = root;
            this.Directories = [];
            this.Files = [];
            Directory.GetDirectories(root).ToList().ForEach(dir =>
            {
                Directories.Add(new FileSystem(dir));
            });
            this.Files = [.. Directory.GetFiles(root)];
            this.CurrrentDirectory = Path.GetFileName(root);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileSystem"/> class with the specified root directory and file search pattern.
        /// </summary>
        /// <param name="root"></param>
        /// <param name="fileSearchPattern"></param>
        public FileSystem(string root, string fileSearchPattern)
        {
            this.Root = root;
            this.Directories = [];
            this.Files = [];
            Directory.GetDirectories(root).ToList().ForEach(dir =>
            {
                Directories.Add(new FileSystem(dir,fileSearchPattern));
            });
            this.Files = [.. Directory.GetFiles(root)];
            this.CurrrentDirectory = Path.GetFileName(root);
            this.FileSearchPattern = fileSearchPattern;
        }

        /// <summary>
        /// Gets the root directory of the file system.
        /// </summary>
        public string? Root { get; }

        /// <summary>
        /// Gets the name of the directory represented by this file system instance.
        /// </summary>
        public string? CurrrentDirectory { get; }

        /// <summary>
        /// Gets the file search pattern used to filter files in the directory.
        /// </summary>
        public string? FileSearchPattern { get; }

        /// <summary>
        /// Gets the list of directories in the file system.
        /// </summary>
        public IList<IFileSystem>? Directories { get; }

        /// <summary>
        /// Gets the list of files in the file system.
        /// </summary>
        public IList<string>? Files { get; }

        /// <summary>
        /// Gets the list of files in the current directory without any search pattern.
        /// </summary>
        /// <param name="directoryName"></param>
        /// <returns></returns>
        public IFileSystem? GetSubDirectory(string directoryName)
        {
            return this.Directories?.FirstOrDefault(a => a.CurrrentDirectory?.Equals(directoryName, StringComparison.InvariantCultureIgnoreCase) ?? false);
        }

        /// <summary>
        /// Gets the list of files in the current directory without any search pattern.
        /// </summary>
        /// <returns></returns>
        public IList<string>? GetFiles()
        {
            return [.. Directory.GetFiles(this.Root ?? string.Empty)];
        }

        /// <summary>
        /// Gets the list of files in the current directory with the specified search pattern.
        /// </summary>
        /// <returns></returns>
        public IList<string>? GetFilesWithSearchPattern()
        {
            if (string.IsNullOrEmpty(FileSearchPattern))
            {
                return GetFiles();
            }
            return [.. Directory.GetFiles(this.Root ?? string.Empty, FileSearchPattern)];
        }

        /// <summary>
        /// Gets the list of files in the specified directory with the specified search pattern.
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
        public IList<string>? GetFilesWithSearchPattern(string directory)
        {
            if (string.IsNullOrEmpty(FileSearchPattern))
            {
                return [.. Directory.GetFiles(directory)];
            }
            return [.. Directory.GetFiles(directory, FileSearchPattern)];
        }

        /// <summary>
        /// Get the workspace directory based on the provided workspace name.
        /// </summary>
        /// <param name="workspace"></param>
        /// <returns></returns>
        public IFileSystem? GetWorkspace(string workspace)
        {
            if (string.IsNullOrEmpty(workspace))
                workspace = FileSystemConstants.DefaultWorkspace;
            string workspacePath = Path.Combine(Root ?? string.Empty, workspace);
            if (!Directory.Exists(workspacePath))
                Directory.CreateDirectory(workspacePath);
            return new FileSystem(workspacePath);
        }
    }
}
