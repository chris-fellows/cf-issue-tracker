using System.ComponentModel.DataAnnotations;

namespace CFIssueTrackerCommon.Models
{
    public class SystemTaskType
    {
        [Required]
        public string Id { get; set; } = String.Empty;

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = String.Empty;
    }
}
