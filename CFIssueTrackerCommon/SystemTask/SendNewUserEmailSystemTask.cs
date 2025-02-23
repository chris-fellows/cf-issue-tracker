using CFIssueTrackerCommon.Constants;
using CFIssueTrackerCommon.Email;
using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;
using Microsoft.Extensions.DependencyInjection;

namespace CFIssueTrackerCommon.SystemTask
{
    /// <summary>
    /// Sends New User emails that have been requested as system task jobs
    /// </summary>
    public class SendNewUserEmailSystemTask : ISystemTask
    {
        public static string TaskName => SystemTaskTypeNames.SendNewUserEmail;

        public string Name => TaskName;

        public async Task ExecuteAsync(Dictionary<string, object> parameters, IServiceProvider serviceProvider, CancellationToken cancellationToken)
        {
            var emailService = serviceProvider.GetRequiredService<IEmailService>();
            var passwordResetService = serviceProvider.GetRequiredService<IPasswordResetService>();
            var systemTaskJobService = serviceProvider.GetRequiredService<ISystemTaskJobService>();
            var systemTaskStatusService = serviceProvider.GetRequiredService<ISystemTaskStatusService>();
            var systemTaskTypeService = serviceProvider.GetRequiredService<ISystemTaskTypeService>();
            var systemValueTypeService = serviceProvider.GetRequiredService<ISystemValueTypeService>();
            var userService = serviceProvider.GetRequiredService<IUserService>();

            // Get system task statuses
            var systemTaskStatusPending = await systemTaskStatusService.GetByNameAsync(SystemTaskStatusNames.Pending);
            var systemTaskStatusActive = await systemTaskStatusService.GetByNameAsync(SystemTaskStatusNames.Active);
            var systemTaskStatusCompletedSuccess = await systemTaskStatusService.GetByNameAsync(SystemTaskStatusNames.CompletedSuccess);
            var systemTaskStatusCompletedError = await systemTaskStatusService.GetByNameAsync(SystemTaskStatusNames.CompletedError);

            var systemTaskType = await systemTaskTypeService.GetByNameAsync(SystemTaskTypeNames.SendNewUserEmail);

            var systemValueTypePasswordResetId = await systemValueTypeService.GetByNameAsync(SystemValueTypeNames.PasswordResetId);
            var systemValueTypeUserId = await systemValueTypeService.GetByNameAsync(SystemValueTypeNames.UserId);        

            // Get system task jobs for Pending status
            var systemTaskJobFilter = new SystemTaskJobFilter()
            {
                StatusIds = new List<string>() { systemTaskStatusPending.Id },
                TypeIds = new List<string>() { systemTaskType.Id }
            };
            var systemTaskJobs = (await systemTaskJobService.GetByFilterAsync(systemTaskJobFilter)).OrderBy(s => s.CreatedDateTime).ToList();

            // Execute jobs
            var emailContent = new NewUserEmailContent();
            while (systemTaskJobs.Any() && !cancellationToken.IsCancellationRequested)
            {
                var systemTaskJob = systemTaskJobs.First();
                systemTaskJobs.Remove(systemTaskJob);

                await SendEmail(systemTaskJob,
                                emailContent, emailService,
                                passwordResetService, systemTaskJobService, systemValueTypeService,
                                systemTaskStatusActive,
                                systemTaskStatusCompletedSuccess,
                                systemTaskStatusCompletedError,
                                systemValueTypePasswordResetId, systemValueTypeUserId,
                                userService,
                                cancellationToken);
            }
        }

        /// <summary>
        /// Sends email
        /// </summary>
        /// <param name="systemTaskJob"></param>
        /// <param name="emailContent"></param>
        /// <param name="emailService"></param>
        /// <param name="passwordResetService"></param>
        /// <param name="systemValueTypeService"></param>
        /// <param name="systemValueTypePasswordResetId"></param>
        /// <param name="systemValueTypeUserId"></param>
        /// <param name="userService"></param>
        /// <returns></returns>
        private async Task SendEmail(SystemTaskJob systemTaskJob,
                                            IEmailContent emailContent,
                                            IEmailService emailService,
                                            IPasswordResetService passwordResetService,
                                            ISystemTaskJobService systemTaskJobService,
                                            ISystemValueTypeService systemValueTypeService,
                                            SystemTaskStatus systemTaskStatusActive,
                                            SystemTaskStatus systemTaskStatusCompletedSuccess,
                                            SystemTaskStatus systemTaskStatusCompletedError,
                                            SystemValueType systemValueTypePasswordResetId,
                                            SystemValueType systemValueTypeUserId,
                                            IUserService userService,
                                            CancellationToken cancellationToken)
        {
            // Set job status Active
            systemTaskJob.StatusId = systemTaskStatusActive.Id;
            await systemTaskJobService.UpdateAsync(systemTaskJob);

            // Get password reset            
            var userId = systemTaskJob.Parameters.First(p => p.SystemValueTypeId == systemValueTypeUserId.Id).Value;

            // Get user
            var user = await userService.GetByIdAsync(userId);

            // Create email
            var emailParameters = new Dictionary<string, string>();
            var body = emailContent.GetBody(emailParameters);

            // Send email
            await emailService.SendAsync(new List<string>() { user.Email }, new(),
                                       body, "Welcome to CF Issue Tracker");

            // Set job status Completed Succcess
            systemTaskJob.StatusId = systemTaskStatusCompletedSuccess.Id;
            await systemTaskJobService.UpdateAsync(systemTaskJob);
        }
    }
}
