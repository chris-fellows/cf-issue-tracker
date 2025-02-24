﻿using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFIssueTrackerCommon.Notifications
{
    /// <summary>
    /// Notification of audit event by Slack
    /// </summary>
    public class SlackNotification : INotificationService
    {
        public Task NotifyAsync(AuditEvent auditEvent)
        {
            return Task.CompletedTask;
        }
    }
}
