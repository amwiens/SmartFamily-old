using System.ComponentModel.DataAnnotations;

namespace SmartFamily.Core.Models
{
    public class FileSettings : DomainObject
    {
        [Required]
        public string? Name { get; set; }

        public string? Value { get; set; }
    }
}