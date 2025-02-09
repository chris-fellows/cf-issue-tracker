using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFIssueTrackerCommon.Models
{
    /// <summary>
    /// Audit event
    /// </summary>
    public class AuditEvent
    {
        public string Id { get; set; } = String.Empty;

        public string TypeId { get; set; } = String.Empty;

        public DateTimeOffset CreatedDateTime { get; set; }
    }
}
