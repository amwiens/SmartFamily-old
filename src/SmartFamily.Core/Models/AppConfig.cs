namespace SmartFamily.Core.Models
{
    /// <summary>
    /// Application configuration.
    /// </summary>
    public class AppConfig
    {
        /// <summary>
        /// Folder for the configuration file.
        /// </summary>
        public string ConfigurationsFolder { get; set; }

        /// <summary>
        /// Folder for logging.
        /// </summary>
        public string LoggingFolder { get; set; }

        /// <summary>
        /// File name for the properties file.
        /// </summary>
        public string AppPropertiesFileName { get; set; }

        /// <summary>
        /// File name for the recent files file.
        /// </summary>
        public string RecentFilesFileName { get; set; }

        /// <summary>
        /// URL for the privacy statement
        /// </summary>
        public string PrivacyStatement { get; set; }
    }
}