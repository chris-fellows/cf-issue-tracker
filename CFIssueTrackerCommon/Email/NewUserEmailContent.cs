using CFIssueTrackerCommon.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFIssueTrackerCommon.Email
{
    /// <summary>
    /// Content for new user email
    /// </summary>
    public class NewUserEmailContent : IEmailContent
    {
        public string Name => "NewUser";

        public string GetBody(Dictionary<string, string> parameters)
        {
            // TODO: Set this
            var body = new StringBuilder("<html>" +
                                    "<head>" +
                                    "</head>" +
                                    "<body>" +
                                    $"Welcome to CF Issue Tracker." +                                 
                                    "</body>" +
                                    "</html>");

            return body.ToString();
        }
    }
}
