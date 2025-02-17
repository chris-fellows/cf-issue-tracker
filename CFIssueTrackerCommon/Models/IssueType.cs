using System.ComponentModel.DataAnnotations;

namespace CFIssueTrackerCommon.Models
{
    /// <summary>
    /// Issue type
    /// </summary>
    public class IssueType
    {
        public string Id { get; set; } = String.Empty;

        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = String.Empty;
    }
}
