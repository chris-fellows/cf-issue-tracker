using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFIssueTrackerCommon.Models
{
    /// <summary>
    /// Reference to user
    /// </summary>
    public class UserReference
    {
        [MaxLength(50)]
        public string Id { get; set; } = String.Empty;

        [Required]
        [MaxLength(50)]
        [ForeignKey("User")]
        public string UserId { get; set; } = String.Empty;

        [DeleteBehavior(DeleteBehavior.NoAction)]
        public virtual User? User { get; set; }
    }
}
