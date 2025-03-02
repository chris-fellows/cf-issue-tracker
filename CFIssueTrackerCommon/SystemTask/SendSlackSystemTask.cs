using CFIssueTrackerCommon.Constants;
using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFIssueTrackerCommon.SystemTask
{
    /// <summary>
    /// Sends Slack messages that were scheduled as system task jobs
    /// </summary>
    public class SendSlackSystemTask : ISystemTask
    {        
        public string Name => SystemTaskTypeNames.SendSlack;

        public async Task ExecuteAsync(Dictionary<string, object> parameters, IServiceProvider serviceProvider, CancellationToken cancellationToken)
        {
            // Get services
            var auditEventService = serviceProvider.GetRequiredService<IAuditEventService>();
            var auditEventTypeService = serviceProvider.GetRequiredService<IAuditEventTypeService>();            
            var slackService = serviceProvider.GetRequiredService<ISlackService>();
            var systemTaskJobService = serviceProvider.GetRequiredService<ISystemTaskJobService>();
            var systemTaskStatusService = serviceProvider.GetRequiredService<ISystemTaskStatusService>();
            var systemTaskTypeService = serviceProvider.GetRequiredService<ISystemTaskTypeService>();
            var systemValueTypeService = serviceProvider.GetRequiredService<ISystemValueTypeService>();
            var userService = serviceProvider.GetRequiredService<IUserService>();

            var currentUser = (await userService.GetAllAsync()).First(u => u.GetUserType() == Enums.UserTypes.System);

            // Get system task statuses
            var systemTaskStatusPending = await systemTaskStatusService.GetByNameAsync(SystemTaskStatusNames.Pending);
            var systemTaskStatusActive = await systemTaskStatusService.GetByNameAsync(SystemTaskStatusNames.Active);
            var systemTaskStatusCompletedSuccess = await systemTaskStatusService.GetByNameAsync(SystemTaskStatusNames.CompletedSuccess);
            var systemTaskStatusCompletedError = await systemTaskStatusService.GetByNameAsync(SystemTaskStatusNames.CompletedError);

            // Get system task type
            var systemTaskTypeSend = await systemTaskTypeService.GetByNameAsync(SystemTaskTypeNames.SendSlack);

            // Get value type for Slack creator
            var systemValueTypeCreator = await systemValueTypeService.GetByNameAsync(SystemValueTypeNames.SlackCreator);

            // Get system task jobs for Pending status
            var systemTaskJobFilter = new SystemTaskJobFilter()
            {
                StatusIds = new List<string>() { systemTaskStatusPending.Id },
                TypeIds = new List<string>() { systemTaskTypeSend.Id }
            };
            var systemTaskJobs = (await systemTaskJobService.GetByFilterAsync(systemTaskJobFilter)).OrderBy(s => s.CreatedDateTime).ToList();

            // Execute jobs
            while (systemTaskJobs.Any() && !cancellationToken.IsCancellationRequested)
            {
                var systemTaskJob = systemTaskJobs.First();
                systemTaskJobs.Remove(systemTaskJob);

                // Get Slack creator
                var parameterCreator = systemTaskJob.Parameters.First(p => p.SystemValueTypeId == systemValueTypeCreator.Id);
                var creator = serviceProvider.GetRequiredKeyedService<ISlackCreator>(parameterCreator.Value);

                try
                {
                    // Send Slack
                    await Send(systemTaskJob,
                                    creator,
                                    slackService,
                                    auditEventService, auditEventTypeService,
                                    systemTaskJobService, systemValueTypeService,
                                    systemTaskStatusActive,
                                    systemTaskStatusCompletedSuccess,
                                    systemTaskStatusCompletedError,                                    
                                    currentUser,
                                    cancellationToken);
                }
                catch (Exception exception)
                {
                    // TODO: Log error
                }
            }
        }

        /// <summary>
        /// Sends Slack notification
        /// </summary>
        /// <param name="systemTaskJob"></param>
        /// <param name="creator"></param>
        /// <param name="slackService"></param>
        /// <param name="passwordResetService"></param>
        /// <param name="systemValueTypeService"></param>        
        /// <param name="userService"></param>
        /// <returns></returns>
        private async Task Send(SystemTaskJob systemTaskJob,
                                            ISlackCreator creator,
                                            ISlackService slackService,
                                            IAuditEventService auditEventService,
                                            IAuditEventTypeService auditEventTypeService,                                            
                                            ISystemTaskJobService systemTaskJobService,
                                            ISystemValueTypeService systemValueTypeService,
                                            SystemTaskStatus systemTaskStatusActive,
                                            SystemTaskStatus systemTaskStatusCompletedSuccess,
                                            SystemTaskStatus systemTaskStatusCompletedError,
                                            User currentUser,
                                            CancellationToken cancellationToken)
        {
            // Set job status Active
            systemTaskJob.StatusId = systemTaskStatusActive.Id;
            await systemTaskJobService.UpdateAsync(systemTaskJob);

            // Get system values for parameters
            var systemValues = systemTaskJob.Parameters.Select(p => p.ToSystemValue()).ToList();

            // TODO: Send Slack message

            // Add audit event
            await AddAuditEvenSentAsync(creator, auditEventService, auditEventTypeService, systemValueTypeService, currentUser);

            // Set job status Completed Succcess
            systemTaskJob.StatusId = systemTaskStatusCompletedSuccess.Id;
            await systemTaskJobService.UpdateAsync(systemTaskJob);
        }

        private async Task AddAuditEvenSentAsync(ISlackCreator creator,
                                   IAuditEventService auditEventService,
                                   IAuditEventTypeService auditEventTypeService,
                                   ISystemValueTypeService systemValueTypeService,
                                       User currentUser)
        {
            var auditEventType = await auditEventTypeService.GetByNameAsync(AuditEventTypeNames.SentSlack);
            var systemValueTypeCreator = await systemValueTypeService.GetByNameAsync(SystemValueTypeNames.SlackCreator);

            var auditEvent = new AuditEvent()
            {
                Id = Guid.NewGuid().ToString(),
                CreatedDateTime = DateTimeOffset.UtcNow,
                CreatedUserId = currentUser.Id,
                TypeId = auditEventType.Id,
                Parameters = new List<AuditEventParameter>()
                    {
                        new AuditEventParameter()
                        {
                            Id = Guid.NewGuid().ToString(),
                            SystemValueTypeId = systemValueTypeCreator.Id,
                            Value = creator.Name
                        }
                    }
            };
            await auditEventService.AddAsync(auditEvent);
        }
    }
}
