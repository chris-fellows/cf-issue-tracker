using CFIssueTrackerCommon.Models;

namespace CFIssueTrackerCommon.Interfaces
{
    /// <summary>
    /// Processes audit event. E.g. Creates a notification.
    /// </summary>
    public interface IAuditEventProcessorService
    {
        Task ProcessAsync(AuditEvent auditEvent);
    }
}
