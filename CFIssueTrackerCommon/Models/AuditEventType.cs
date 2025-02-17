using System.ComponentModel.DataAnnotations;

namespace CFIssueTrackerCommon.Models
{
    /// <summary>
    /// Audit event type
    /// </summary>
    public class AuditEventType
    {
        public string Id { get; set; } = String.Empty;

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = String.Empty;
    }
}
