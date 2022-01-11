namespace SmartFamily.Core.Contracts.Services
{
    /// <summary>
    /// File service interface.
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// Reads the file.
        /// </summary>
        /// <typeparam name="T">Type of the file.</typeparam>
        /// <param name="folderPath">Folder path to the file.</param>
        /// <param name="fileName">File name.</param>
        /// <returns>File</returns>
        T Read<T>(string folderPath, string fileName);

        /// <summary>
        /// Saves the file.
        /// </summary>
        /// <typeparam name="T">Type of the file.</typeparam>
        /// <param name="folderPath">Folder path to the file.</param>
        /// <param name="fileName">File name.</param>
        /// <param name="content">Content to save.</param>
        void Save<T>(string folderPath, string fileName, T content);

        /// <summary>
        /// Deletes the file.
        /// </summary>
        /// <param name="folderPath">Folder path to the file.</param>
        /// <param name="fileName">File name.</param>
        void Delete(string folderPath, string fileName);
    }
}