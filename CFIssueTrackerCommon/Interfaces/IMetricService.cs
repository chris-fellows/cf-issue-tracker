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
        Task<Metrics> GetIssueCountByProjectMetricsAsync(IssueFilter filter);

        Task<Metrics> GetIssueCountByStatusMetricsAsync(IssueFilter filter);

        Task<Metrics> GetIssueCountByTypeMetricsAsync(IssueFilter filter);

        Task<Metrics> GetIssueCountByAssignedUserMetricsAsync(IssueFilter filter);

        Task<Metrics> GetIssueCountByCreatedUserMetricsAsync(IssueFilter filter);
    }
}
