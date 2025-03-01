using CFIssueTrackerCommon.Constants;
using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NuGet.Packaging;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFIssueTrackerCommon.Services
{
    public class AuditEventProcessorService : IAuditEventProcessorService
    {
        private readonly IAuditEventTypeService _auditEventTypeService;
        private readonly INotificationGroupService _notificationGroupService;
        private readonly ISystemTaskJobService _systemTaskJobService;
        private readonly ISystemTaskStatusService _systemTaskStatusService;
        private readonly ISystemTaskTypeService _systemTaskTypeService;
        private readonly ISystemValueTypeService _systemValueTypeService;
        private readonly IUserService _userService;

        public AuditEventProcessorService(IAuditEventTypeService auditEventTypeService)
        {
            _auditEventTypeService = auditEventTypeService;
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
        }               

        //private async Task ProcessMSTeams(AuditEvent auditEvent, AuditEventType auditEventType, MSTeamsNotificationConfig msTeamsNotificationConfig)
        //{

        //}

        //private async Task ProcessSlack(AuditEvent auditEvent, AuditEventType auditEventType, SlackNotificationConfig slackNotificationConfig)
        //{

        //}
    }
}
