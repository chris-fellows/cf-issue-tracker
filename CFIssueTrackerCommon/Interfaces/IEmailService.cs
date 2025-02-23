namespace CFIssueTrackerCommon.Interfaces
{
    /// <summary>
    /// Email service
    /// </summary>
    public interface IEmailService
    {
        Task SendAsync(List<string> recipientEmails, List<string> ccEmails, string body, string subject);
    }
}
