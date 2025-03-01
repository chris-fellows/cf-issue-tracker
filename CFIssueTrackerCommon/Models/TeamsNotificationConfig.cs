using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFIssueTrackerCommon.Models
{
    /// <summary>
    /// Config for Teams notification
    /// </summary>
    public class TeamsNotificationConfig
    {
        [MaxLength(50)]
        public string Id { get; set; } = String.Empty;

        /// <summary>
        /// Creator of Teams notification
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Creator { get; set; } = String.Empty;

        [Required]
        [MaxLength(200)]
        public string Url { get; set; } = String.Empty;
    }
}
