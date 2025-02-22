using CFIssueTrackerCommon.Models;

namespace CFIssueTrackerCommon.Interfaces
{
    public interface ISystemTaskJobService : IEntityWithIdService<SystemTaskJob, string>
    {
        Task<List<SystemTaskJob>> GetByFilterAsync(SystemTaskJobFilter filter);
    }
}
