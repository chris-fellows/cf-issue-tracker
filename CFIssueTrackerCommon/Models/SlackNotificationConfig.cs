using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFIssueTrackerCommon.Models
{
    /// <summary>
    /// Config for Slack notification
    /// </summary>
    public class SlackNotificationConfig
    {
        [MaxLength(50)]
        public string Id { get; set; } = String.Empty;

        /// <summary>
        /// Creator of Slack notification
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Creator { get; set; } = String.Empty;

        [Required]
        [MaxLength(200)]
        public string Url { get; set; } = String.Empty;
    }
}
