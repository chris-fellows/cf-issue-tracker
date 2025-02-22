using CFIssueTrackerCommon.Interfaces;
using System.Text;

namespace CFIssueTrackerCommon.Email
{
    /// <summary>
    /// Content for Reset Password email
    /// </summary>
    public class ResetPasswordEmailContent : IEmailContent
    {
        public string Name => "ResetPassword";

        public string GetBody(Dictionary<string, string> parameters)
        {            
            // TODO: Set this
            var body = new StringBuilder("<html>" +
                                    "<head>" +
                                    "</head>" +
                                    "<body>" +
                                    $"A request to reset the password for {(string)parameters["Email"]} has been made." +
                                    $"<a href='{(string)parameters["URL"]}'>Click here</a>" +
                                    "</body>" +
                                    "</html>");

            return body.ToString();
        }
    }
}
