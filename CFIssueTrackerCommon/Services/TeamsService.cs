using CFIssueTrackerCommon.Interfaces;

namespace CFIssueTrackerCommon.Services
{
    public class TeamsService : ITeamsService
    {
        public Task SendAsync()
        {
            return Task.CompletedTask;
        }
    }
}
