using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFIssueTrackerCommon.Notifications
{
    /// <summary>
    /// Notification of audit event by email
    /// </summary>
    public class EmailNotification : INotificationService
    {
        public Task NotifyAsync(AuditEvent auditEvent)
        {
            return Task.CompletedTask;
        }
    }
}
