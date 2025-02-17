using System.ComponentModel.DataAnnotations;

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
        public string ProjectId { get; set; } = String.Empty;

        /// <summary>
        /// Project component
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string ProjectComponentId { get; set; } = String.Empty;

        /// <summary>
        /// Issue type
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string TypeId { get; set; } = String.Empty;

        /// <summary>
        /// Issue status
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string StatusId { get; set; } = String.Empty;

        /// <summary>
        /// User who created issue
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string CreatedUserId { get; set; } = String.Empty;        

        /// <summary>
        /// Time issue created
        /// </summary>
        public DateTimeOffset CreatedDateTime { get; set; } = DateTimeOffset.UtcNow;

        /// <summary>
        /// User who issue is currently assigned to
        /// </summary>
        [MaxLength(50)]
        public string AssignedUserId { get; set; } = String.Empty;

        /// <summary>
        /// Time that issue was assigned to user
        /// </summary>
        public DateTimeOffset? AssignedDateTime { get; set; }

        ///// <summary>
        ///// Users tracking issue
        ///// </summary>
        //public List<string> TrackingUserIds = new List<string>();
    }
}
