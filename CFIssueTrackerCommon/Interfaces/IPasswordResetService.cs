using CFIssueTrackerCommon.Models;

namespace CFIssueTrackerCommon.Interfaces
{
    public interface IPasswordResetService : IEntityWithIdService<PasswordReset, string>
    {
        Task<PasswordReset?> GetByUserIdAsync(string id);
    }
}
