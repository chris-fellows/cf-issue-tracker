using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFIssueTrackerCommon.Models
{
    /// <summary>
    /// Project component
    /// </summary>
    public class ProjectComponent
    {
        [MaxLength(50)]
        public string Id { get; set; } = String.Empty;

        [Required]
        [MaxLength(50)]
        [ForeignKey("Project")]
        public string ProjectId { get; set; } = String.Empty;

        [DeleteBehavior(DeleteBehavior.NoAction)]
        public virtual Project? Project { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = String.Empty;

        [Required]
        [MaxLength(50)]
        public string Color { get; set; } = String.Empty;

        [Required]
        [MaxLength(50)]
        public string ImageSource { get; set; } = String.Empty;
    }
}
