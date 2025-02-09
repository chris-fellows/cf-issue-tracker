using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFIssueTrackerCommon.Models
{
    /// <summary>
    /// Audit event type
    /// </summary>
    public class AuditEventType
    {
        public string Id { get; set; } = String.Empty;

        public string Name { get; set; } = String.Empty;
    }
}
