namespace DevNest.Common.Manager.FileSystem
{
    /// <summary>
    /// Interface representing a file system structure.
    /// </summary>
    public interface IFileSystem
    {
        /// <summary>
        /// Gets the root directory of the file system.
        /// </summary>
        string? Root { get; }

        /// <summary>
        /// Gets the name of the directory represented by this file system instance.
        /// </summary>
        string? CurrrentDirectory { get; }

        /// <summary>
        /// Gets the file search pattern used to filter files in the directory.
        /// </summary>
        string? FileSearchPattern { get; }

        /// <summary>
        /// Gets the list of directories in the file system.
        /// </summary>
        IList<IFileSystem>? Directories { get; }

        /// <summary>
        /// Gets the list of files in the file system.
        /// </summary>
        IList<string>? Files { get; }

        /// <summary>
        /// Gets the list of files in the current directory.
        /// </summary>
        /// <returns></returns>
        IList<string>? GetFilesWithSearchPattern();

        /// <summary>
        /// Gets the list of files in the current directory with the specified search pattern.
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
        IList<string>? GetFilesWithSearchPattern(string directory);

        /// <summary>
        /// Gets the list of files in the current directory without any search pattern.
        /// </summary>
        /// <param name="directoryName"></param>
        /// <returns></returns>
        IFileSystem? GetSubDirectory(string directoryName);

        /// <summary>
        /// Gets the list of files in the current directory without any search pattern.
        /// </summary>
        /// <param name="workspace"></param>
        /// <returns></returns>
        IFileSystem? GetWorkspace(string workspace);
    }
}
