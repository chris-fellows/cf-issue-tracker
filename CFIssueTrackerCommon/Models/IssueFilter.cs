namespace CFIssueTrackerCommon.Models
{
    /// <summary>
    /// Issue filter
    /// </summary>
    public class IssueFilter
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
        /// Created User Ids
        /// </summary>
        public List<string>? CreatedUserIds { get; set; }

        /// <summary>
        /// Project Ids
        /// </summary>
        public List<string>? ProjectIds { get; set; }

        /// <summary>
        /// Issue Status Ids
        /// </summary>
        public List<string>? IssueStatusIds { get; set; }

        /// <summary>
        /// Issue Type Ids
        /// </summary>
        public List<string>? IssueTypeIds { get; set; }
    }
}
