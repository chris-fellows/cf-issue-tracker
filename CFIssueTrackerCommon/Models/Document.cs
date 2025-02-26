using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFIssueTrackerCommon.Models
{
    /// <summary>
    /// Document
    /// </summary>
    public class Document
    {
        [MaxLength(50)]
        public string Id { get; set; } = String.Empty;

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = String.Empty;

        [Required]
        public byte[] Content { get; set; } = new byte[0];        
    }
}
