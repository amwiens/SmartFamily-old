﻿namespace SmartFamily.Core.Models
{
    /// <summary>
    /// Application config
    /// </summary>
    public class AppConfig
    {
        /// <summary>
        /// Folder for the configuration file.
        /// </summary>
        public string ConfigurationsFolder { get; set; }

        /// <summary>
        /// File name for the properties file.
        /// </summary>
        public string AppPropertiesFileName { get; set; }

        /// <summary>
        /// URL for the privacy statement
        /// </summary>
        public string PrivacyStatement { get; set; }
    }
}