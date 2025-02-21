using System.ComponentModel.DataAnnotations;

namespace CFIssueTrackerCommon.Models
{
    /// <summary>
    /// Issue type
    /// </summary>
    public class IssueType
    {
        [MaxLength(50)]
        public string Id { get; set; } = String.Empty;

        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = String.Empty;
    }
}
