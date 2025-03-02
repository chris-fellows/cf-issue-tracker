using System.ComponentModel.DataAnnotations;

namespace CFIssueTrackerCommon.Models
{
    /// <summary>
    /// Document. E.g. Issue document.
    /// </summary>
    public class Document
    {
        [MaxLength(50)]
        public string Id { get; set; } = String.Empty;

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = String.Empty;
        
        [Required]
        [MaxLength(100000)]
        public byte[] Content { get; set; } = new byte[0];        
    }
}
