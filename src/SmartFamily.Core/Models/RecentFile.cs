namespace SmartFamily.Core.Models
{
    /// <summary>
    /// Recent files.
    /// </summary>
    public class RecentFile
    {
        /// <summary>
        /// Gets or sets the file name.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the file path.
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Gets or sets the last opened date/time.
        /// </summary>
        public DateTime LastOpened { get; set; }
    }
}