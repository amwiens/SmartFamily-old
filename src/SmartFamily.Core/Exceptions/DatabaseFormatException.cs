namespace SmartFamily.Core.Exceptions
{
    /// <summary>
    /// Database format exception.
    /// </summary>
    public class DatabaseFormatException : Exception
    {
        /// <summary>
        /// File name.
        /// </summary>
        public string FileName { get; }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="fileName">File name.</param>
        public DatabaseFormatException(string fileName)
        {
            FileName = fileName;
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="fileName">File name.</param>
        public DatabaseFormatException(string? message, string fileName) : base(message)
        {
            FileName = fileName;
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="innerException">Inner exception.</param>
        /// <param name="fileName">File name.</param>
        public DatabaseFormatException(string? message, Exception? innerException, string fileName)
        {
            FileName = fileName;
        }
    }
}