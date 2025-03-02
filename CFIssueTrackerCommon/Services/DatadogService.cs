using CFIssueTrackerCommon.Interfaces;

namespace CFIssueTrackerCommon.Services
{
    public class DatadogService : IDatadogService
    {
        public Task SendAsync()
        {
            return Task.CompletedTask;
        }
    }
}
