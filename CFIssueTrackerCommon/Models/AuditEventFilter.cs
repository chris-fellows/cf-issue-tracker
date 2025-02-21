using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFIssueTrackerCommon.Models
{
    /// <summary>
    /// Audit event filter
    /// </summary>
    public class AuditEventFilter
    {  
        /// <summary>
       /// Event created from
       /// </summary>
        public DateTimeOffset? CreatedDateTimeFrom { get; set; }

        /// <summary>
        /// Event created up to
        /// </summary>
        public DateTimeOffset? CreatedDateTimeTo { get; set; }

        /// <summary>
        /// Event User Ids
        /// </summary>
        public List<string>? CreatedUserIds { get; set; }

        /// <summary>
        /// Audit Event Type Ids
        /// </summary>
        public List<string>? AuditEventTypeIds { get; set; }

        /// <summary>
        /// Issue Ids
        /// </summary>
        public List<string>? IssueIds { get; set; }
    }
}
