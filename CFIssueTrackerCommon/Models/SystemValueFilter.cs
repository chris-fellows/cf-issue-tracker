namespace CFIssueTrackerCommon.Models
{
    /// <summary>
    /// Filter on system value. 
    /// 
    /// E.g. TypeId=IssueId, Values=[IssueId1,IssueId2]
    /// </summary>    
    public class SystemValueFilter
    {
        /// <summary>
        /// System value type
        /// </summary>
        public string TypeId { get; set; } = String.Empty;

        /// <summary>
        /// System values
        /// </summary>
        public List<string> Values { get; set; } = new();
    }
}
