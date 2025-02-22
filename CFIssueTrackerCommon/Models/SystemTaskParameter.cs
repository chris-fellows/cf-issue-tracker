using System.ComponentModel.DataAnnotations;

namespace CFIssueTrackerCommon.Models
{
    /// <summary>
    ///System task parameter
    /// </summary>
    public class SystemTaskParameter
    {
        [MaxLength(50)]
        public string Id { get; set; } = String.Empty;

        /// <summary>
        /// System Value Type Id
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string SystemValueTypeId { get; set; } = String.Empty;

        /// <summary>
        /// Value
        /// </summary>
        [Required]
        [MaxLength(500)]
        public string Value { get; set; } = String.Empty;
    }
}
