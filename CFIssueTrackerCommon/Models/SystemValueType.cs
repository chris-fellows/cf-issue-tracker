using System.ComponentModel.DataAnnotations;

namespace CFIssueTrackerCommon.Models
{
    /// <summary>
    /// System value type.
    /// 
    /// Allows us to handle lots of different types of values without having to add new properties.
    /// </summary>
    public class SystemValueType
    {
        [MaxLength(50)]
        public string Id { get; set; } = String.Empty;

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = String.Empty;
    }
}
