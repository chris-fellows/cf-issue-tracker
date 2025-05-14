using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CFIssueTrackerCommon.Models;
using Microsoft.AspNetCore.SignalR.Client;

namespace CFIssueTrackerCommon.Interfaces
{
    public interface ISystemEventSubscriberService
    {
        string SubscribeAuditEventAdded(Action<AuditEvent> action);

        void Unsubscribe(string subscribeId);
    }
}
