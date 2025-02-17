using System.ComponentModel.DataAnnotations;

namespace CFIssueTrackerCommon.Models
{
    /// <summary>
    /// Project component
    /// </summary>
    public class ProjectComponent
    {
        public string Id { get; set; } = String.Empty;

        [Required]
        [MaxLength(50)]
        public string ProjectId { get; set; } = String.Empty;

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = String.Empty;
    }
}
