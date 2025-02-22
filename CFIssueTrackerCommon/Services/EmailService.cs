using CFIssueTrackerCommon.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFIssueTrackerCommon.Services
{
    public class EmailService : IEmailService
    {
        public Task SendAsync(List<string> recipientEmails, List<string> ccEmails, string subject, string body)
        {
            throw new NotImplementedException();
        }
    }
}
