using CFIssueTrackerCommon.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CFIssueTrackerCommon.SystemTask
{
    /// <summary>
    /// Generates notifications
    /// </summary>
    public class NotificationSystemTask : ISystemTask
    {
        public static string TaskName => "Notification";

        public string Name => TaskName;

        public async Task ExecuteAsync(Dictionary<string, object> parameters, IServiceProvider serviceProvider, CancellationToken cancellationToken)
        {
            //var auditEventTypeService = serviceProvider.GetService<IAuditEventTypeService>();

        }
    }
}
