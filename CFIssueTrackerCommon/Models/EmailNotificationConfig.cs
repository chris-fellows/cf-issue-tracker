using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFIssueTrackerCommon.Models
{
    /// <summary>
    /// Config for email notification
    /// </summary>
    public class EmailNotificationConfig
    {
        [MaxLength(50)]
        public string Id { get; set; } = String.Empty;

        /// <summary>
        /// Email creator to create emails
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Creator { get; set; } = String.Empty;

        /// <summary>
        /// Recipient email list. For some emails then the recipients may be indicated elsewhere.
        /// E.g. Reset Password email is sent to user that reset password.
        /// </summary>
        [MaxLength(200)]
        public string RecipientEmails { get; set; } = String.Empty;
    }
}
