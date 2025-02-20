using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFIssueTrackerCommon.Models
{
    public class IssueComment
    {
        public string Id { get; set; } = String.Empty;

        [Required]
        public string Description { get; set; } = String.Empty;

        [Required]
        [MaxLength(50)]
        public string IssueId { get; set; } = String.Empty;

        [Required]
        [MaxLength(50)]
        public string CreatedUserId { get; set; } = String.Empty;

        public DateTimeOffset CreatedDateTime = DateTimeOffset.UtcNow;
    }
}
