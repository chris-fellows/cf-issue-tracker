using CFIssueTrackerCommon.Enums;

namespace CFIssueTrackerCommon.Models
{
    /// <summary>
    /// Audit event filter
    /// </summary>
    public class AuditEventFilter
    {  
        /// <summary>
       /// Event created from
       /// </summary>
        public DateTimeOffset? CreatedDateTimeFrom { get; set; }

        /// <summary>
        /// Event created up to
        /// </summary>
        public DateTimeOffset? CreatedDateTimeTo { get; set; }

        /// <summary>
        /// Event User Ids
        /// </summary>
        public List<string>? CreatedUserIds { get; set; }

        /// <summary>
        /// Audit Event Type Ids
        /// </summary>
        public List<string>? AuditEventTypeIds { get; set; }

        /// <summary>
        /// Issue Ids
        /// </summary>
        public List<string>? IssueIds { get; set; }

        /// <summary>
        /// Logical operator for Parameters. Allows us to handle any filter valid or all filters valid
        /// </summary>
        public LogicalOperators ParametersLogicalOperator { get; set; } = LogicalOperators.And;

        /// <summary>
        /// Audit event parameter(s)
        /// 
        /// E.g. TypeId=IssueId, Values=[IssueId1,IssueId2]
        /// </summary>
        public List<SystemValueFilter>? Parameters { get; set; }
    }
}
