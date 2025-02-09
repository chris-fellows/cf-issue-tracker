using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string Reference { get; set; } = String.Empty;

        /// <summary>
        /// Project
        /// </summary>
        public string ProjectId { get; set; } = String.Empty;

        /// <summary>
        /// Project component
        /// </summary>
        public string ProjectComponentId { get; set; } = String.Empty;

        /// <summary>
        /// Issue type
        /// </summary>
        public string TypeId { get; set; } = String.Empty;

        /// <summary>
        /// Issue status
        /// </summary>
        public string StatusId { get; set; } = String.Empty;

        /// <summary>
        /// User who created issue
        /// </summary>
        public string CreatedUserId { get; set; } = String.Empty;

        /// <summary>
        /// Time issue created
        /// </summary>
        public DateTimeOffset CreatedDateTime { get; set; } = DateTimeOffset.UtcNow;
    }
}
