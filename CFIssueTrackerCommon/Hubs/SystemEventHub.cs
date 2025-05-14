using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CFIssueTrackerCommon.Models;
using Microsoft.AspNetCore.SignalR;

namespace CFIssueTrackerCommon.Hubs
{
    public class SystemEventHub : Hub<ISystemEventHubClient>
    {
        private readonly IServiceProvider _serviceProvider;
        
        public SystemEventHub(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task SendAuditEventAdded(AuditEvent auditEvent)
        {
            await Clients.All.ReceiveAuditEventAdded(auditEvent);
        }
    }
}
