namespace CFIssueTrackerCommon.Models
{
    /// <summary>
    /// Audit event
    /// </summary>
    public class AuditEvent
    {
        public string Id { get; set; } = String.Empty;

        public string TypeId { get; set; } = String.Empty;

        public DateTimeOffset CreatedDateTime { get; set; }

        //public List<AuditEventParameter> Parameters = new List<AuditEventParameter>();
    }
}
