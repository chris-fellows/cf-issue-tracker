using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFIssueTrackerCommon.Models
{
    /// <summary>
    /// Issue
    /// </summary>
    public class Issue
    {
        /// <summary>
        /// Unique Id
        /// </summary>
        [MaxLength(50)]
        public string Id { get; set; } = String.Empty;

        /// <summary>
        /// Issue reference
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Reference { get; set; } = String.Empty;

        /// <summary>
        /// Issue description
        /// </summary>
        [Required]
        public string Description { get; set; } = String.Empty;

        /// <summary>
        /// Project
        /// </summary>
        [Required]
        [MaxLength(50)]
        [ForeignKey("Project")]        
        public string ProjectId { get; set; } = String.Empty;

        [DeleteBehavior(DeleteBehavior.NoAction)]
        public virtual Project? Project { get; set; }

        /// <summary>
        /// Project component
        /// </summary>
        [Required]
        [MaxLength(50)]
        [ForeignKey("ProjectComponent")]        
        public string ProjectComponentId { get; set; } = String.Empty;

        [DeleteBehavior(DeleteBehavior.NoAction)]
        public virtual ProjectComponent? ProjectComponent { get; set; }

        /// <summary>
        /// Issue type
        /// </summary>
        [Required]
        [MaxLength(50)]
        [ForeignKey("Type")]        
        public string TypeId { get; set; } = String.Empty;

        [DeleteBehavior(DeleteBehavior.NoAction)]
        public virtual IssueType? Type { get; set; }

        /// <summary>
        /// Issue status
        /// </summary>
        [Required]       
        [MaxLength(50)]
        [ForeignKey("Status")]        
        public string StatusId { get; set; } = String.Empty;

        [DeleteBehavior(DeleteBehavior.NoAction)]
        public virtual IssueStatus? Status { get; set; }

        /// <summary>
        /// User who created issue
        /// </summary>
        [Required]
        [MaxLength(50)]
        [ForeignKey("User")]        
        public string CreatedUserId { get; set; } = String.Empty;

        [DeleteBehavior(DeleteBehavior.NoAction)]
        public virtual User? CreatedUser { get; set; }

        /// <summary>
        /// Time issue created
        /// </summary>
        public DateTimeOffset CreatedDateTime { get; set; } = DateTimeOffset.UtcNow;

        /// <summary>
        /// User who issue is currently assigned to
        /// </summary>
        [MaxLength(50)]
        [ForeignKey("User")]        
        public string AssignedUserId { get; set; } = String.Empty;

        [DeleteBehavior(DeleteBehavior.NoAction)]
        public virtual User? AssignedUser { get; set; }

        /// <summary>
        /// Time that issue was assigned to user
        /// </summary>
        public DateTimeOffset? AssignedDateTime { get; set; }

        /// <summary>
        /// Documents
        /// </summary>
        public ICollection<DocumentReference> Documents { get; set; }

        /// <summary>
        /// Tags
        /// </summary>
        public ICollection<TagReference> Tags { get; set; }

        /// <summary>
        /// Users who are tracking issue
        /// </summary>
        public ICollection<UserReference> TrackingUsers { get; set; }
    }
}
