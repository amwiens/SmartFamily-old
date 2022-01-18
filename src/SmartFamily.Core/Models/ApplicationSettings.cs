using SmartFamily.Core.Models;

namespace SmartFamily.Core
{
    /// <summary>
    /// Application settings.
    /// </summary>
    public static class ApplicationSettings
    {
        /// <summary>
        /// The database that is currently open.
        /// </summary>
        public static string OpenDatabase { get; set; }

        /// <summary>
        /// The list of recent files that have been opened.
        /// </summary>
        public static List<RecentFile> RecentFiles { get; set; }
    }
}