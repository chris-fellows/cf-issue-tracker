﻿using CFIssueTrackerCommon.Constants;
using CFIssueTrackerCommon.EntityReader;
using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;
using CFIssueTrackerCommon.Services;

namespace CFIssueTracker.Data
{
    /// <summary>
    /// Loads seed data from IEntityReader(s)    
    /// </summary>
    public class SeedLoader
    {
        /// <summary>
        /// Deletes all data
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        public async Task DeleteAsync(IServiceScope scope)
        {
            // Get services
            var auditEventService = scope.ServiceProvider.GetRequiredService<IAuditEventService>();
            var auditEventTypeService = scope.ServiceProvider.GetRequiredService<IAuditEventTypeService>();
            var contentTemplateService = scope.ServiceProvider.GetRequiredService<IContentTemplateService>();
            var documentService = scope.ServiceProvider.GetRequiredService<IDocumentService>();
            var issueCommentService = scope.ServiceProvider.GetRequiredService<IIssueCommentService>();
            var issueService = scope.ServiceProvider.GetRequiredService<IIssueService>();
            var issueStatusService = scope.ServiceProvider.GetRequiredService<IIssueStatusService>();
            var issueTypeService = scope.ServiceProvider.GetRequiredService<IIssueTypeService>();
            var metricsTypeService = scope.ServiceProvider.GetRequiredService<IMetricsTypeService>();
            var notificationGroupService = scope.ServiceProvider.GetRequiredService<INotificationGroupService>();
            var passwordResetService = scope.ServiceProvider.GetRequiredService<IPasswordResetService>();
            var projectComponentService = scope.ServiceProvider.GetRequiredService<IProjectComponentService>();
            var projectService = scope.ServiceProvider.GetRequiredService<IProjectService>();
            var systemTaskStatusService = scope.ServiceProvider.GetRequiredService<ISystemTaskStatusService>();
            var systemTaskTypeService = scope.ServiceProvider.GetRequiredService<ISystemTaskTypeService>();
            var systemValueTypeService = scope.ServiceProvider.GetRequiredService<ISystemValueTypeService>();
            var tagService = scope.ServiceProvider.GetRequiredService<ITagService>();
            var userService = scope.ServiceProvider.GetRequiredService<IUserService>();

            // Delete transaction data
            var auditEvents = await auditEventService.GetAllAsync();
            foreach(var auditEvent in auditEvents)
            {
                await auditEventService.DeleteByIdAsync(auditEvent.Id);
            }

            var issueComments = await issueCommentService.GetAllAsync();
            foreach (var issueComment in issueComments)
            {
                await issueCommentService.DeleteByIdAsync(issueComment.Id);
            }

            var issues = await issueService.GetAllAsync();
            foreach (var issue in issues)
            {
                await issueService.DeleteByIdAsync(issue.Id);
            }

            var passwordResets = await passwordResetService.GetAllAsync();
            foreach (var passwordReset in passwordResets)
            {
                await passwordResetService.DeleteByIdAsync(passwordReset.Id);
            }

            // Delete static data
            var projectComponents = await projectComponentService.GetAllAsync();
            foreach(var projectComponent in projectComponents)
            {
                await projectComponentService.DeleteByIdAsync(projectComponent.Id);
            }

            var projects = await projectService.GetAllAsync();
            foreach(var project in projects)
            {
                await projectService.DeleteByIdAsync(project.Id);
            }

            var users = await userService.GetAllAsync();
            foreach(var user in users)
            {
                await userService.DeleteByIdAsync(user.Id);
            }

            // Delete static data
            var issueStatuses = await issueStatusService.GetAllAsync();
            foreach(var issueStatus in issueStatuses)
            {
                await issueStatusService.DeleteByIdAsync(issueStatus.Id);
            }

            var issueTypes = await issueTypeService.GetAllAsync();
            foreach (var issueType in issueTypes)
            {
                await issueTypeService.DeleteByIdAsync(issueType.Id);
            }

            var auditEventTypes = await auditEventTypeService.GetAllAsync();
            foreach (var auditEventType in auditEventTypes)
            {
                await auditEventTypeService.DeleteByIdAsync(auditEventType.Id);
            }

            var notificationGroups = await notificationGroupService.GetAllAsync();
            foreach (var notificationGroup in notificationGroups)
            {
                await notificationGroupService.DeleteByIdAsync(notificationGroup.Id);
            }

            var metricsTypes = await metricsTypeService.GetAllAsync();
            foreach (var metricsType in metricsTypes)
            {
                await metricsTypeService.DeleteByIdAsync(metricsType.Id);
            }

            var systemTaskTypes = await systemTaskTypeService.GetAllAsync();
            foreach (var systemTaskType in systemTaskTypes)
            {
                await systemTaskTypeService.DeleteByIdAsync(systemTaskType.Id);
            }

            var systemValueTypes = await systemValueTypeService.GetAllAsync();
            foreach (var systemValueType in systemValueTypes)
            {
                await systemValueTypeService.DeleteByIdAsync(systemValueType.Id);
            }

            var documents = await documentService.GetAllAsync();
            foreach(var document in documents)
            {
                await documentService.DeleteByIdAsync(document.Id);
            }

            var tags = await tagService.GetAllAsync();
            foreach (var tag in tags)
            {
                await tagService.DeleteByIdAsync(tag.Id);
            }

            var contentTemplates = await contentTemplateService.GetAllAsync();
            foreach(var contentTemplate in contentTemplates)
            {
                await contentTemplateService.DeleteByIdAsync(contentTemplate.Id);
            }
        }

        /// <summary>
        /// Loads seed data. Throws exception if any data exists.
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="randomIssuesToCreate"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task LoadAsync(IServiceScope scope, int randomIssuesToCreate)
        {
            // Get services
            var auditEventService = scope.ServiceProvider.GetRequiredService<IAuditEventService>();
            var auditEventTypeService = scope.ServiceProvider.GetRequiredService<IAuditEventTypeService>();
            var contentTemplateService = scope.ServiceProvider.GetRequiredService<IContentTemplateService>();
            var documentService = scope.ServiceProvider.GetRequiredService<IDocumentService>();
            var issueCommentService = scope.ServiceProvider.GetRequiredService<IIssueCommentService>();
            var issueService = scope.ServiceProvider.GetRequiredService<IIssueService>();
            var issueStatusService = scope.ServiceProvider.GetRequiredService<IIssueStatusService>();
            var issueTypeService = scope.ServiceProvider.GetRequiredService<IIssueTypeService>();
            var metricsTypeService = scope.ServiceProvider.GetRequiredService<IMetricsTypeService>();
            var notificationGroupService = scope.ServiceProvider.GetRequiredService<INotificationGroupService>();
            var projectComponentService = scope.ServiceProvider.GetRequiredService<IProjectComponentService>();
            var projectService = scope.ServiceProvider.GetRequiredService<IProjectService>();
            var systemTaskStatusService = scope.ServiceProvider.GetRequiredService<ISystemTaskStatusService>();
            var systemTaskTypeService = scope.ServiceProvider.GetRequiredService<ISystemTaskTypeService>();
            var systemValueTypeService = scope.ServiceProvider.GetRequiredService<ISystemValueTypeService>();
            var tagService = scope.ServiceProvider.GetRequiredService<ITagService>();
            var userService = scope.ServiceProvider.GetRequiredService<IUserService>();

            // Get seed data services
            var auditEventTypeSeed = scope.ServiceProvider.GetRequiredKeyedService<IEntityReader<AuditEventType>>("AuditEventTypeSeed");
            var contentTemplateSeed = scope.ServiceProvider.GetRequiredKeyedService<IEntityReader<ContentTemplate>>("ContentTemplateSeed");
            var issueStatusSeed = scope.ServiceProvider.GetRequiredKeyedService<IEntityReader<IssueStatus>>("IssueStatusSeed");
            var issueTypeSeed = scope.ServiceProvider.GetRequiredKeyedService<IEntityReader<IssueType>>("IssueTypeSeed");
            var metricsTypeSeed = scope.ServiceProvider.GetRequiredKeyedService<IEntityReader<MetricsType>>("MetricsTypeSeed");
            var notificationGroupSeed = scope.ServiceProvider.GetRequiredKeyedService<IEntityReader<NotificationGroup>>("NotificationGroupSeed");
            var projectComponentSeed = scope.ServiceProvider.GetRequiredKeyedService<IEntityReader<ProjectComponent>>("ProjectComponentSeed");
            var projectSeed = scope.ServiceProvider.GetRequiredKeyedService<IEntityReader<Project>>("ProjectSeed");
            var systemTaskStatusSeed = scope.ServiceProvider.GetRequiredKeyedService<IEntityReader<SystemTaskStatus>>("SystemTaskStatusSeed");
            var systemTaskTypeSeed = scope.ServiceProvider.GetRequiredKeyedService<IEntityReader<SystemTaskType>>("SystemTaskTypeSeed");
            var systemValueTypeSeed = scope.ServiceProvider.GetRequiredKeyedService<IEntityReader<SystemValueType>>("SystemValueTypeSeed");
            var tagSeed = scope.ServiceProvider.GetRequiredKeyedService<IEntityReader<Tag>>("TagSeed");
            var userSeed = scope.ServiceProvider.GetRequiredKeyedService<IEntityReader<User>>("UserSeed");

            // Check that no data exists
            var auditEventTypesOld = await auditEventTypeService.GetAllAsync();
            if (auditEventTypesOld.Any())
            {
                throw new ArgumentException("Cannot load seed data because data already exists");
            }

            // Add system value types
            var systemValueTypesNew = systemValueTypeSeed.Read();
            foreach (var systemValueType in systemValueTypesNew)
            {
                await systemValueTypeService.AddAsync(systemValueType);
            }

            // Add system task statuses
            var systemTaskStatusNew = systemTaskStatusSeed.Read();
            foreach (var systemTaskStatus in systemTaskStatusNew)
            {
                var systemTaskStatusResult = await systemTaskStatusService.AddAsync(systemTaskStatus);
            }

            // Add system task types
            var systemTaskTypesNew = systemTaskTypeSeed.Read();
            foreach (var systemTaskType in systemTaskTypesNew)
            {
                var systemTaskTypeResult = await systemTaskTypeService.AddAsync(systemTaskType);
            }

            // Add notification groups templates
            var notificationGroupsNew = notificationGroupSeed.Read();
            foreach (var notificationGroup in notificationGroupsNew)
            {
                await notificationGroupService.AddAsync(notificationGroup);
            }

            // Add audit event types (Depends on notification groups existing)
            var auditEventTypesNew = auditEventTypeSeed.Read();
            foreach (var auditEventType in auditEventTypesNew)
            {
                await auditEventTypeService.AddAsync(auditEventType);
            }

            // Add audit event types
            var tagsNew = tagSeed.Read();
            foreach (var tag in tagsNew)
            {
                await tagService.AddAsync(tag);
            }

            // Add content templates (E.g. Email notifications)
            var contentTemplatesNew = contentTemplateSeed.Read();
            foreach(var contentTemplate in contentTemplatesNew)
            {
                await contentTemplateService.AddAsync(contentTemplate);
            }
         
            // Get audit event types & system value types
            var auditEventTypes = await auditEventTypeService.GetAllAsync();
            var systemValueTypes = await systemValueTypeService.GetAllAsync();

            // Add users. We can't add the audit event in this loop because we been to log the UserId for the
            // System user
            var usersNew = userSeed.Read();
            foreach (var user in usersNew)
            {
                var userResult = await userService.AddAsync(user);
            }

            // Get all users
            var users = await userService.GetAllAsync();
            var systemUser = users.First(u => u.GetUserType() == CFIssueTrackerCommon.Enums.UserTypes.System);

            // Add audit event for users
            foreach(var user in users)
            {
                var auditEvent = new AuditEvent()
                {
                    Id = Guid.NewGuid().ToString(),
                    CreatedDateTime = DateTimeOffset.UtcNow,
                    CreatedUserId = systemUser.Id,
                    TypeId = auditEventTypes.First(i => i.Name == AuditEventTypeNames.UserCreated).Id,
                    Parameters = new List<AuditEventParameter>()
                    {
                        new AuditEventParameter()
                        {
                            Id = Guid.NewGuid().ToString(),
                            SystemValueTypeId = systemValueTypes.First(i => i.Name == SystemValueTypeNames.UserId).Id,
                            Value = user.Id
                        }
                    }
                };
                await auditEventService.AddAsync(auditEvent);
            }

            // Add issue statuses
            var issueStatusesNew = issueStatusSeed.Read();
            foreach (var issueStatus in issueStatusesNew)
            {
                var issueStatusResult = await issueStatusService.AddAsync(issueStatus);

                var auditEvent = new AuditEvent()
                {
                    Id = Guid.NewGuid().ToString(),
                    CreatedDateTime = DateTimeOffset.UtcNow,
                    CreatedUserId = systemUser.Id,
                    TypeId = auditEventTypes.First(i => i.Name == AuditEventTypeNames.IssueStatusCreated).Id,
                    Parameters = new List<AuditEventParameter>()
                    {
                        new AuditEventParameter()
                        {
                            Id = Guid.NewGuid().ToString(),
                            SystemValueTypeId = systemValueTypes.First(i => i.Name == SystemValueTypeNames.IssueStatusId).Id,
                            Value = issueStatusResult.Id
                        }
                    }
                };
                await auditEventService.AddAsync(auditEvent);               
            }

            // Add issue types
            var issueTypesNew = issueTypeSeed.Read();
            foreach (var issueType in issueTypesNew)
            {
                var issueTypeResult = await issueTypeService.AddAsync(issueType);

                var auditEvent = new AuditEvent()
                {
                    Id = Guid.NewGuid().ToString(),
                    CreatedDateTime = DateTimeOffset.UtcNow,
                    CreatedUserId = systemUser.Id,
                    TypeId = auditEventTypes.First(i => i.Name == AuditEventTypeNames.IssueTypeCreated).Id,
                    Parameters = new List<AuditEventParameter>()
                    {
                        new AuditEventParameter()
                        {
                            Id = Guid.NewGuid().ToString(),
                            SystemValueTypeId = systemValueTypes.First(i => i.Name == SystemValueTypeNames.IssueTypeId).Id,
                            Value = issueTypeResult.Id
                        }
                    }
                };
                await auditEventService.AddAsync(auditEvent);
            }

            // Add metric types
            var metricsTypes = metricsTypeSeed.Read();
            foreach (var metricsType in metricsTypes)
            {
                await metricsTypeService.AddAsync(metricsType);

                //var auditEvent = new AuditEvent()
                //{
                //    Id = Guid.NewGuid().ToString(),
                //    CreatedDateTime = DateTimeOffset.UtcNow,
                //    TypeId = auditEventTypes.First(i => i.Name == AuditEventTypeNames.M).Id,
                //    Parameters = new List<AuditEventParameter>()
                //    {
                //        new AuditEventParameter()
                //        {
                //            SystemValueTypeId = systemValueTypes.First(i => i.Name == SystemValueTypeNames.IssueTypeId).Id,
                //            Value = issueTypeResult.Id
                //        }
                //    }
                //};
                //await auditEventService.AddAsync(auditEvent);
            }

            // Add projects
            var projectsNew = projectSeed.Read();
            foreach (var project in projectsNew)
            {
                var projectResult =  await projectService.AddAsync(project);

                var auditEvent = new AuditEvent()
                {
                    Id = Guid.NewGuid().ToString(),
                    CreatedDateTime = DateTimeOffset.UtcNow,
                    CreatedUserId = systemUser.Id,
                    TypeId = auditEventTypes.First(i => i.Name == AuditEventTypeNames.ProjectCreated).Id,
                    Parameters = new List<AuditEventParameter>()
                    {
                        new AuditEventParameter()
                        {
                            Id = Guid.NewGuid().ToString(),
                            SystemValueTypeId = systemValueTypes.First(i => i.Name == SystemValueTypeNames.ProjectId).Id,
                            Value = projectResult.Id
                        }
                    }
                };
                await auditEventService.AddAsync(auditEvent);
            }

            // Add project components
            var projectComponentsNew = projectComponentSeed.Read();
            foreach (var projectComponent in projectComponentsNew)
            {
                projectComponent.ProjectId = projectsNew.ToList()[0].Id;
                var projectComponentResult = await projectComponentService.AddAsync(projectComponent);

                var auditEvent = new AuditEvent()
                {
                    Id = Guid.NewGuid().ToString(),
                    CreatedDateTime = DateTimeOffset.UtcNow,
                    CreatedUserId = systemUser.Id,
                    TypeId = auditEventTypes.First(i => i.Name == AuditEventTypeNames.ProjectComponentCreated).Id,
                    Parameters = new List<AuditEventParameter>()
                    {
                        new AuditEventParameter()
                        {
                            Id = Guid.NewGuid().ToString(),
                            SystemValueTypeId = systemValueTypes.First(i => i.Name == SystemValueTypeNames.ProjectComponentId).Id,
                            Value = projectComponentResult.Id
                        }
                    }
                };
                await auditEventService.AddAsync(auditEvent);
            }

            // Add random issues so that we have some data
            if (randomIssuesToCreate > 0)
            {                
                var issueCreator = new RandomIssueCreator(auditEventService, auditEventTypeService,
                    documentService,
                    issueCommentService, issueService,
                    issueStatusService, issueTypeService,
                    projectComponentService, projectService,
                    systemValueTypeService, tagService,
                    userService);

                await issueCreator.CreateIssuesAsync(randomIssuesToCreate);
            }
        }     
    }
}
