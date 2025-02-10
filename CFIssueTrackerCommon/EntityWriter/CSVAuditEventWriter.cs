using CFIssueTrackerCommon.Models;

namespace CFIssueTrackerCommon.EntityWriter
{
    public class CSVAuditEventWriter : IEntityWriter<AuditEvent>
    {
        public void Write(IEnumerable<AuditEvent> auditEvents)
        {
            foreach (var auditEvent in auditEvents)
            {
                Write(auditEvent);
            }
        }

        private void Write(AuditEvent auditEvent)
        {

        }
    }
}
