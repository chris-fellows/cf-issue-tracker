using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [ForeignKey("AuditEventType")]        
        public string TypeId { get; set; } = String.Empty;

        [DeleteBehavior(DeleteBehavior.NoAction)]
        public virtual AuditEventType? AuditEventType { get; set; }

        public DateTimeOffset CreatedDateTime { get; set; }

        /// <summary>
        /// Parameters
        /// </summary>
        public ICollection<AuditEventParameter> Parameters { get; set; }
    }
}
