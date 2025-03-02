using CFIssueTrackerCommon.Interfaces;

namespace CFIssueTrackerCommon.Services
{
    public class SlackService : ISlackService
    {
        public Task SendAsync()
        {
            return Task.CompletedTask;
        }
    }
}
