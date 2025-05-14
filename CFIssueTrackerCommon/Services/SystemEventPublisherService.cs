using CFIssueTrackerCommon.Hubs;
using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;
using Microsoft.AspNetCore.SignalR;

namespace CFIssueTrackerCommon.Services
{
    public class SystemEventPublisherService : ISystemEventPublisherService
    {
        private readonly IHubContext<SystemEventHub> _hubContext;

        public SystemEventPublisherService(IHubContext<SystemEventHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task AuditEventAddedAsync(AuditEvent auditEvent)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveAuditEventAdded", auditEvent);
        }
    }
}
