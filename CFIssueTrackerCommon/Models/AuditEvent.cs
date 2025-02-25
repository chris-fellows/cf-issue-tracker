using System.ComponentModel.DataAnnotations;

namespace CFIssueTrackerCommon.Models
{
    /// <summary>
    /// Audit event
    /// </summary>
    public class AuditEvent
    {
        [MaxLength(50)]
        public string Id { get; set; } = String.Empty;

        [Required]
        [MaxLength(50)]
        public string TypeId { get; set; } = String.Empty;

        public DateTimeOffset CreatedDateTime { get; set; }

        /// <summary>
        /// Parameters
        /// </summary>
        public ICollection<AuditEventParameter> Parameters { get; set; }
    }
}
