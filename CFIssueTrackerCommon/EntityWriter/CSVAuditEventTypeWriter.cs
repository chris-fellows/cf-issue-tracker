using CFIssueTrackerCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFIssueTrackerCommon.EntityWriter
{
    public class CSVAuditEventTypeWriter : IEntityWriter<AuditEventType>
    {
        public void Write(IEnumerable<AuditEventType> auditEventTypes)
        {
            foreach (var auditEventType in auditEventTypes)
            {
                Write(auditEventType);
            }
        }

        private void Write(AuditEventType auditEventType)
        {

        }
    }
}
