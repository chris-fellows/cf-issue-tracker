using CFIssueTrackerCommon.Constants;
using CFIssueTrackerCommon.Email;
using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;
using Microsoft.Extensions.DependencyInjection;

namespace CFIssueTrackerCommon.SystemTask
{
    /// <summary>
    /// Sends Issue Assigned emails that have been requested as system task jobs
    /// </summary>
    public class SendIssueAssignedEmailSystemTask : ISystemTask
    {
        public static string TaskName => SystemTaskTypeNames.SendIssueAssignedEmail;

        public string Name => TaskName;

        public async Task ExecuteAsync(Dictionary<string, object> parameters, IServiceProvider serviceProvider, CancellationToken cancellationToken)
        {
            var emailService = serviceProvider.GetRequiredService<IEmailService>();
            var issueService = serviceProvider.GetRequiredService<IIssueService>();
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

            var systemTaskType = await systemTaskTypeService.GetByNameAsync(SystemTaskTypeNames.SendIssueAssignedEmail);
            
            var systemValueTypeIssueId = await systemValueTypeService.GetByNameAsync(SystemValueTypeNames.IssueId);           

            // Get system task jobs for Pending status
            var systemTaskJobFilter = new SystemTaskJobFilter()
            {
                StatusIds = new List<string>() { systemTaskStatusPending.Id },
                TypeIds = new List<string>() { systemTaskType.Id }
            };
            var systemTaskJobs = (await systemTaskJobService.GetByFilterAsync(systemTaskJobFilter)).OrderBy(s => s.CreatedDateTime).ToList();

            // Execute jobs
            var emailContent = new IssueAssignedEmailContent();
            while (systemTaskJobs.Any() && !cancellationToken.IsCancellationRequested)
            {
                var systemTaskJob = systemTaskJobs.First();
                systemTaskJobs.Remove(systemTaskJob);

                await SendEmail(systemTaskJob,
                                emailContent, emailService,
                                issueService,
                                systemTaskJobService, systemValueTypeService,
                                systemTaskStatusActive,
                                systemTaskStatusCompletedSuccess,
                                systemTaskStatusCompletedError,
                                systemValueTypeIssueId,
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
                                            IIssueService issueService,
                                            ISystemTaskJobService systemTaskJobService,
                                            ISystemValueTypeService systemValueTypeService,
                                            SystemTaskStatus systemTaskStatusActive,
                                            SystemTaskStatus systemTaskStatusCompletedSuccess,
                                            SystemTaskStatus systemTaskStatusCompletedError,                                            
                                            SystemValueType systemValueTypeIssueId,
                                            IUserService userService,
                                            CancellationToken cancellationToken)
        {
            // Set job status Active
            systemTaskJob.StatusId = systemTaskStatusActive.Id;
            await systemTaskJobService.UpdateAsync(systemTaskJob);

            // Get issue
            var issueId = systemTaskJob.Parameters.First(p => p.SystemValueTypeId == systemValueTypeIssueId.Id).Value;            
            var issue = await issueService.GetByIdAsync(issueId);

            // Get issue user(s)
            var assignedUser = await userService.GetByIdAsync(issue.AssignedUserId);
            var createdUser = await userService.GetByIdAsync(issue.CreatedUserId);

            if (String.IsNullOrEmpty(issue.AssignedUserId))    // Issue not assigned
            {
                // Set job status Completed Error
                systemTaskJob.StatusId = systemTaskStatusCompletedError.Id;
                systemTaskJob.Error = "Issue not assigned";
                await systemTaskJobService.UpdateAsync(systemTaskJob);
            }
            else if (String.IsNullOrEmpty(assignedUser.Email))    // Assigned user has no email (System user?)
            {
                // Set job status Completed Error
                systemTaskJob.StatusId = systemTaskStatusCompletedError.Id;
                systemTaskJob.Error = "User email not set";
                await systemTaskJobService.UpdateAsync(systemTaskJob);
            }
            else
            { 
                // Create email
                var emailParameters = new Dictionary<string, string>();
                var body = emailContent.GetBody(emailParameters);

                // Send email
                var recipientEmails = new List<string>() { assignedUser.Email };
                var ccEmails = assignedUser.Id == createdUser.Id ? new List<string>() : new List<string>() { createdUser.Email };               

                await emailService.SendAsync(recipientEmails, ccEmails,
                                           "Issue Assigned", body);

                // Set job status Completed Succcess
                systemTaskJob.StatusId = systemTaskStatusCompletedSuccess.Id;
                await systemTaskJobService.UpdateAsync(systemTaskJob);
            }
        }
    }
}
