using CFIssueTrackerCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFIssueTrackerCommon.Hubs
{
    public interface ISystemEventHubClient
    {
        Task ReceiveAuditEventAdded(AuditEvent auditEvent);
    }
}
