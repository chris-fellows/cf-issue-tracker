using CFIssueTrackerCommon.Models;

namespace CFIssueTrackerCommon.Interfaces
{
    /// <summary>
    /// Interface for metrics
    /// </summary>
    public interface IMetricService
    {
        Metrics GetIssueCountByProjectMetrics();

        Metrics GetIssueCountByStatusMetrics();

        Metrics GetIssueCountByTypeMetrics();

        Metrics GetIssueCountByAssignedUserMetrics();

        Metrics GetIssueCountByCreatedUserMetrics();
    }
}
