using CFIssueTrackerCommon.Models;

namespace CFIssueTrackerCommon.Interfaces
{
    /// <summary>
    /// Interface for metrics
    /// 
    /// TODO: Support time period dimensions
    /// </summary>
    public interface IMetricService
    {
        //Task<Metrics> GetIssueCountByProjectMetricsAsync(IssueFilter filter);

        //Task<Metrics> GetIssueCountByStatusMetricsAsync(IssueFilter filter);

        //Task<Metrics> GetIssueCountByTypeMetricsAsync(IssueFilter filter);

        //Task<Metrics> GetIssueCountByAssignedUserMetricsAsync(IssueFilter filter);

        //Task<Metrics> GetIssueCountByCreatedUserMetricsAsync(IssueFilter filter);

        //Task<Metrics> GetIssueAssignedCountByUserAndStatusMetricsAsync(IssueFilter issueFilter);

        /// <summary>
        /// Gets issue count by one or more property dimensions. E.g. By Status / By Project & Tag        
        /// </summary>
        /// <param name="issueFilter"></param>
        /// <param name="propertyNameDimensions"></param>
        /// <param name="excludeZeroValues">Exclude zero metrics (If false then returns every combination of dimensions)</param>
        /// <returns></returns>
        Task<Metrics> GetIssueCountAsync(IssueFilter issueFilter, List<string> propertyNameDimensions, bool excludeZeroValues);
    }
}
