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
        Task<Metrics> GetIssueCountByProjectMetricsAsync(IssueFilter issueFilter);

        Task<Metrics> GetIssueCountByStatusMetricsAsync(IssueFilter issueFilter);

        Task<Metrics> GetIssueCountByTypeMetricsAsync(IssueFilter issueFilter);

        Task<Metrics> GetIssueCountByAssignedUserMetricsAsync(IssueFilter issueFilter);

        Task<Metrics> GetIssueCountByCreatedUserMetricsAsync(IssueFilter issueFilter);
    }
}
