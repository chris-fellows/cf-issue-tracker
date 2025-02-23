using CFIssueTrackerCommon.Interfaces;
using System.Net.Mail;

namespace CFIssueTrackerCommon.Services
{
    public class EmailService : IEmailService
    {
        private readonly IEmailConfig _emailConfig;
        private SmtpClient? _smtpClient;

        public EmailService(IEmailConfig emailConfig)
        {
            _emailConfig = emailConfig;
        }

        public async Task SendAsync(List<string> recipientEmails, List<string> ccEmails, string body, string subject)
        {
            if (_smtpClient == null)
            {
                _smtpClient = GetSmtpClient(_emailConfig.Server, _emailConfig.Port, _emailConfig.Username, _emailConfig.Password);
            }

            var mail = new MailMessage();
            mail.From = new MailAddress("");
            foreach (var address in recipientEmails)
            {
                mail.To.Add(address);
            }
            mail.IsBodyHtml = true;
            mail.Subject = subject;
            mail.Body = body;
            
            await _smtpClient.SendMailAsync(mail);
        }

        private static SmtpClient GetSmtpClient(string server, int port, string username, string password)
        {
            var smtpClient = new SmtpClient(server, port);
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new System.Net.NetworkCredential(username, password);
            smtpClient.EnableSsl = true;
            return smtpClient;
        }
    }
}
