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
    /// Reference to document
    /// </summary>
    public class DocumentReference
    {
        [MaxLength(50)]
        public string Id { get; set; } = String.Empty;

        [Required]
        [MaxLength(50)]
        [ForeignKey("Document")]        
        public string DocumentId { get; set; } = String.Empty;

        [DeleteBehavior(DeleteBehavior.NoAction)]
        public virtual Document? Document { get; set; }
    }
}
