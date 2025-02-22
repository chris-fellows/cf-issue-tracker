using CFIssueTrackerCommon.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFIssueTrackerCommon.Email
{
    /// <summary>
    /// Content for Issue Assigned email
    /// </summary>
    public class IssueAssignedEmailContent : IEmailContent
    {
        public string Name => "IssueAssigned";

        public string GetBody(Dictionary<string, string> parameters)
        {
            // TODO: Set this
            var body = new StringBuilder("<html>" +
                                    "<head>" +
                                    "</head>" +
                                    "<body>" +
                                    $"The issue has been assigned to..." +
                                    "</body>" +
                                    "</html>");

            return body.ToString();
        }
    }
}
