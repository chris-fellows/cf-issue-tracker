using CFIssueTrackerCommon.Constants;
using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;
using Microsoft.Extensions.DependencyInjection;

namespace CFIssueTrackerCommon.SystemTask
{
    /// <summary>
    /// Sends emails. Requests were created as system task jobs.
    /// </summary>
    public class SendEmailSystemTask : ISystemTask
    {
        public static string TaskName => SystemTaskTypeNames.SendEmail;

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

            // Get system task type
            var systemTaskType = await systemTaskTypeService.GetByNameAsync(SystemTaskTypeNames.SendEmail);            

            // Get value type for email creator
            var systemValueTypeCreator = await systemValueTypeService.GetByNameAsync(SystemValueTypeNames.EmailCreator);

            // Get system task jobs for Pending status
            var systemTaskJobFilter = new SystemTaskJobFilter()
            {
                StatusIds = new List<string>() { systemTaskStatusPending.Id },
                TypeIds = new List<string>() { systemTaskType.Id }
            };
            var systemTaskJobs = (await systemTaskJobService.GetByFilterAsync(systemTaskJobFilter)).OrderBy(s => s.CreatedDateTime).ToList();

            // Execute jobs
            while (systemTaskJobs.Any() && !cancellationToken.IsCancellationRequested)
            {                               
                var systemTaskJob = systemTaskJobs.First();
                systemTaskJobs.Remove(systemTaskJob);

                // Get email creator
                var parameterCreator = systemTaskJob.Parameters.First(p => p.SystemValueTypeId == systemValueTypeCreator.Id);
                var creator = serviceProvider.GetRequiredKeyedService<IEmailCreator>(parameterCreator.Value);

                try
                {
                    // Send email
                    await Send(systemTaskJob,
                                    creator,
                                    emailService,
                                    passwordResetService, systemTaskJobService, systemValueTypeService,
                                    systemTaskStatusActive,
                                    systemTaskStatusCompletedSuccess,
                                    systemTaskStatusCompletedError,
                                    userService,
                                    cancellationToken);
                }
                catch(Exception exception)
                {
                    // TODO: Log error
                }
            }
        }

        /// <summary>
        /// Sends email
        /// </summary>
        /// <param name="systemTaskJob"></param>
        /// <param name="creator"></param>
        /// <param name="emailService"></param>
        /// <param name="passwordResetService"></param>
        /// <param name="systemValueTypeService"></param>        
        /// <param name="userService"></param>
        /// <returns></returns>
        private async Task Send(SystemTaskJob systemTaskJob,
                                            IEmailCreator creator,
                                            IEmailService emailService,
                                            IPasswordResetService passwordResetService,
                                            ISystemTaskJobService systemTaskJobService,
                                            ISystemValueTypeService systemValueTypeService,
                                            SystemTaskStatus systemTaskStatusActive,
                                            SystemTaskStatus systemTaskStatusCompletedSuccess,
                                            SystemTaskStatus systemTaskStatusCompletedError,                                            
                                            IUserService userService,
                                            CancellationToken cancellationToken)
        {
            // Set job status Active
            systemTaskJob.StatusId = systemTaskStatusActive.Id;
            await systemTaskJobService.UpdateAsync(systemTaskJob);

            // Get system values for parameters
            var systemValues = systemTaskJob.Parameters.Select(p => p.ToSystemValue()).ToList();

            // Create email            
            var subject = creator.GetSubject(systemValues);
            var body = creator.GetBody(systemValues);
            var emailRecipients = creator.GetRecipientEmails(systemValues);
            var ccEmails = new List<string>();

            // Send email
            if (emailRecipients.Any())
            {
                await emailService.SendAsync(emailRecipients, ccEmails, body, subject);
            }

            // Set job status Completed Succcess
            systemTaskJob.StatusId = systemTaskStatusCompletedSuccess.Id;
            await systemTaskJobService.UpdateAsync(systemTaskJob);
        }
    }
}
