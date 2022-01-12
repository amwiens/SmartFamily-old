using System.ComponentModel.DataAnnotations;

namespace SmartFamily.Core.Models
{
    /// <summary>
    /// File settings.
    /// </summary>
    public class FileSettings : DomainObject
    {
        /// <summary>
        /// Name of the setting.
        /// </summary>
        [Required]
        public string? Name { get; set; }

        /// <summary>
        /// Value of the setting.
        /// </summary>
        public string? Value { get; set; }
    }
}