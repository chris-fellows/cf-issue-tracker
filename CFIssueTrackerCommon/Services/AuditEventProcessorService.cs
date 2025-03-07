using CFIssueTrackerCommon.Constants;
using CFIssueTrackerCommon.EntityReader;
using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NuGet.Packaging;

namespace CFIssueTrackerCommon.Services
{
    public class AuditEventProcessorService : IAuditEventProcessorService
    {
        private readonly IAuditEventTypeService _auditEventTypeService;
        private readonly IIssueService _issueService;
        private readonly INotificationGroupService _notificationGroupService;
        private readonly ISystemTaskJobService _systemTaskJobService;
        private readonly ISystemTaskStatusService _systemTaskStatusService;
        private readonly ISystemTaskTypeService _systemTaskTypeService;
        private readonly ISystemValueTypeService _systemValueTypeService;
        private readonly IToastService _toastService;
        private readonly IUserService _userService;

        public AuditEventProcessorService(IAuditEventTypeService auditEventTypeService,
                            IIssueService issueService,
                            INotificationGroupService notificationGroupService,
                            ISystemTaskJobService systemTaskJobService,
                            ISystemTaskStatusService systemTaskStatusService,
                            ISystemTaskTypeService systemTaskTypeService,
                            ISystemValueTypeService systemValueTypeService,
                            IToastService toastService,
                            IUserService userService)
        {
            _auditEventTypeService = auditEventTypeService;
            _issueService = issueService;
            _notificationGroupService = notificationGroupService;
            _systemTaskJobService = systemTaskJobService;
            _systemTaskStatusService = systemTaskStatusService;
            _systemTaskTypeService = systemTaskTypeService;
            _systemValueTypeService = systemValueTypeService;
            _toastService = toastService;
            _userService = userService;
        }

        public async Task ProcessAsync(AuditEvent auditEvent)
        {
            // Get audit event type
            var auditEventType = await _auditEventTypeService.GetByIdAsync(auditEvent.TypeId);

            foreach(var notificationGroupReference in auditEventType.NotificationGroups)
            {
                var notificationGroup = await _notificationGroupService.GetByIdAsync(notificationGroupReference.NotificationGroupId);

                // Process email notifications
                foreach (var emailNotificationConfig in notificationGroup.EmailNotificationConfigs)
                {
                    await ProcessEmail(auditEvent, auditEventType, emailNotificationConfig);
                }
            }

            // Notify toast
            await NotifyToastIfRequiredAsync(auditEvent, auditEventType);
        }

        /// <summary>
        /// Creates a toast notification if required
        /// 
        /// TODO: Consider making this configurable
        /// </summary>
        /// <param name="auditEvent"></param>
        /// <param name="auditEventType"></param>
        /// <returns></returns>
        private async Task NotifyToastIfRequiredAsync(AuditEvent auditEvent, AuditEventType auditEventType)
        {
            if (auditEventType.Name == AuditEventTypeNames.IssueCreated)
            {
                var systemValueTypeIssueId = await _systemValueTypeService.GetByNameAsync(SystemValueTypeNames.IssueId);
                var parameter = auditEvent.Parameters.First(p => p.SystemValueTypeId == systemValueTypeIssueId.Id);
                var issue = await _issueService.GetByIdAsync(parameter.Value);
                var createdUser = await _userService.GetByIdAsync(issue.CreatedUserId);

                _toastService.Information($"Issue {issue.Reference} has been created by {createdUser.Name}");
            }
            else if (auditEventType.Name == AuditEventTypeNames.IssueAssigned)
            {
                var systemValueTypeIssueId = await _systemValueTypeService.GetByNameAsync(SystemValueTypeNames.IssueId);
                var parameter = auditEvent.Parameters.First(p => p.SystemValueTypeId == systemValueTypeIssueId.Id);
                var issue = await _issueService.GetByIdAsync(parameter.Value);
                var assignedUser = await _userService.GetByIdAsync(issue.AssignedUserId);

                _toastService.Information($"Issue {issue.Reference} has been assigned to {assignedUser.Name}");
            }
        }

        /// <summary>
        /// Processes audit event for email notification. Creates a system task job to send email.   
        /// </summary>
        /// <param name="auditEvent"></param>
        /// <param name="auditEventType"></param>
        /// <param name="emailNotificationItem"></param>
        /// <returns></returns>
        private async Task ProcessEmail(AuditEvent auditEvent, AuditEventType auditEventType, EmailNotificationConfig emailNotificationConfig)
        {
            var systemTaskType = await _systemTaskTypeService.GetByNameAsync(SystemTaskTypeNames.SendEmail);
            var systemTaskStatus = await _systemTaskStatusService.GetByNameAsync(SystemTaskStatusNames.Pending);
            
            // Get value types for parameters
            var systemValueType2 = await _systemValueTypeService.GetByNameAsync(SystemValueTypeNames.EmailCreator);
            var systemValueType1 = await _systemValueTypeService.GetByNameAsync(SystemValueTypeNames.AuditEventId);
            
            // Create system task job
            var systemTaskJob = new SystemTaskJob()
            {
                 Id = Guid.NewGuid().ToString(),
                 CreatedDateTime= DateTimeOffset.UtcNow, 
                 TypeId = systemTaskType.Id,
                 StatusId = systemTaskStatus.Id,
                 Parameters = new List<SystemTaskParameter>()
                 {
                     new SystemTaskParameter()
                     {
                         Id =Guid.NewGuid().ToString(),
                         SystemValueTypeId = systemValueType2.Id,                          
                         Value = emailNotificationConfig.Creator
                     },
                     new SystemTaskParameter()
                     {
                         Id = Guid.NewGuid().ToString(),
                         SystemValueTypeId = systemValueType1.Id,                         
                         Value = auditEvent.Id
                     },
                 }                                  
            };

            // Add audit event parameters
            systemTaskJob.Parameters.AddRange(auditEvent.Parameters.Select(aep => new SystemTaskParameter()
            {
                Id= Guid.NewGuid().ToString(),
                SystemValueTypeId = aep.SystemValueTypeId,
                Value = aep.Value
            }));

            // Add specific emails. The parameter may contains placeholders for looking up email addresses (E.g. UserId)
            if (!String.IsNullOrWhiteSpace(emailNotificationConfig.RecipientEmails))
            {                
                var recipientEmails = new List<string>();
                foreach(var recipientEmail in emailNotificationConfig.RecipientEmails.Split('\t'))
                {
                    if (recipientEmail.StartsWith("#UserEmailByUserId#\t"))     // #UserEmailByUserId#\t[UserId]
                    {
                        var parameters = recipientEmail.Split('\t');
                        var user = await _userService.GetByIdAsync(parameters[1]);
                        if (user != null && !String.IsNullOrEmpty(user.Email) &&
                            !recipientEmails.Contains(user.Email))
                        {
                            recipientEmails.Add(user.Email);
                        }
                    }
                    else if(recipientEmail.StartsWith("#Email#\t"))     // #Email#\t[Email]
                    {
                        var parameters = recipientEmail.Split('\t');
                        if (!recipientEmails.Contains(parameters[1]))
                        {
                            recipientEmails.Add(parameters[1]);
                        }
                    }
                    else if (!recipientEmails.Contains(recipientEmail))  // Email address
                    {
                        recipientEmails.Add(recipientEmail);
                    }
                }

                if (recipientEmails.Any())
                {
                    var systemValueType3 = await _systemValueTypeService.GetByNameAsync(SystemValueTypeNames.RecipientEmails);

                    systemTaskJob.Parameters.Add(new SystemTaskParameter()
                    {
                        Id = Guid.NewGuid().ToString(),
                        SystemValueTypeId = systemValueType3.Id,
                        Value = String.Join('\t', recipientEmails)
                    });
                }
            }

            // Add system task job
            await _systemTaskJobService.AddAsync(systemTaskJob);
        }               

        //private async Task ProcessMSTeams(AuditEvent auditEvent, AuditEventType auditEventType, MSTeamsNotificationConfig msTeamsNotificationConfig)
        //{

        //}

        //private async Task ProcessSlack(AuditEvent auditEvent, AuditEventType auditEventType, SlackNotificationConfig slackNotificationConfig)
        //{

        //}
    }
}
