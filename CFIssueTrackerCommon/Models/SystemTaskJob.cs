using System.ComponentModel.DataAnnotations;

namespace CFIssueTrackerCommon.Models
{
    /// <summary>
    /// System task job
    /// </summary>
    public class SystemTaskJob
    {
        [MaxLength(50)]
        public string Id { get; set; } = String.Empty;

        /// <summary>
        /// Job status (SystemTaskStatus.Id)
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string StatusId { get; set; } = String.Empty;        

        /// <summary>
        /// Job type (SystemTaskType.Id)
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string TypeId { get; set; } = String.Empty;

        /// <summary>
        /// Message indicating error
        /// </summary>
        [MaxLength(200)]
        public string? Error { get; set; }

        public DateTimeOffset CreatedDateTime { get; set; }

        /// <summary>
        /// Filter on parameters.
        /// </summary>
        public ICollection<SystemTaskParameter> Parameters { get; set; }
    }
}
