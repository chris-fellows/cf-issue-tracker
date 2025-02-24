using CFIssueTrackerCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFIssueTrackerCommon.Interfaces
{
    /// <summary>
    /// Notification service
    /// </summary>
    public interface INotificationService
    {
        Task NotifyAsync(AuditEvent auditEvent);
    }
}
