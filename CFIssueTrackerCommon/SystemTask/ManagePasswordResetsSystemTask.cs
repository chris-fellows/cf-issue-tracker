using CFIssueTrackerCommon.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CFIssueTrackerCommon.SystemTask
{
    /// <summary>
    /// Manages password resets. Deletes old instances that have expired.
    /// </summary>
    public class ManagePasswordResetsSystemTask : ISystemTask
    {
        public static string TaskName => "Manage Password Resets";

        public string Name => TaskName;

        public async Task ExecuteAsync(Dictionary<string, object> parameters, IServiceProvider serviceProvider, CancellationToken cancellationToken)
        {
            var passwordResetService = serviceProvider.GetRequiredService<IPasswordResetService>();

            // Get expired password resets
            var passwordResets = (await passwordResetService.GetAllAsync()).Where(pr => pr.ExpiresDateTime <= DateTimeOffset.UtcNow).ToList();

            // Delete
            while (passwordResets.Any())
            {
                var passwordReset = passwordResets.First();
                passwordResets.Remove(passwordReset);

                await passwordResetService.DeleteByIdAsync(passwordReset.Id);
            }
        }
    }
}
