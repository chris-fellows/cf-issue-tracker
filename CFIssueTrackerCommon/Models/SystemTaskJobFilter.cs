namespace CFIssueTrackerCommon.Models
{
    /// <summary>
    /// System task job filter
    /// </summary>
    public class SystemTaskJobFilter
    {
        /// <summary>
        /// Issues created from
        /// </summary>
        public DateTimeOffset? CreatedDateTimeFrom { get; set; }

        /// <summary>
        /// Issues created up to
        /// </summary>
        public DateTimeOffset? CreatedDateTimeTo { get; set; }

        /// <summary>
        /// System task statuses
        /// </summary>
        public List<string>? StatusIds { get; set; }

        /// <summary>
        /// System task types
        /// </summary>
        public List<string>? TypeIds { get; set; }
    }
}
