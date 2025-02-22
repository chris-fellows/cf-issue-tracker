using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFIssueTrackerCommon.Interfaces
{
    /// <summary>
    /// Email service
    /// </summary>
    public interface IEmailService
    {
        Task SendAsync(List<string> recipientEmails, List<string> ccEmails, string subject, string body);
    }
}
