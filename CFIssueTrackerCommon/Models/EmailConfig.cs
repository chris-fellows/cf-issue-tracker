using CFIssueTrackerCommon.Interfaces;

namespace CFIssueTrackerCommon.Models
{
    public class EmailConfig : IEmailConfig
    {
        public string Server { get; set; } = String.Empty;

        public int Port { get; set; }

        public string Username { get; set; } = String.Empty;

        public string Password { get; set; } = String.Empty;
    }
}
