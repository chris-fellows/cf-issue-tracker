using CFIssueTrackerCommon.Constants;
using CFIssueTrackerCommon.EntityReader;
using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;
using CFIssueTrackerCommon.Services;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Net;
using static System.Formats.Asn1.AsnWriter;

namespace CFIssueTracker.Data
{
    /// <summary>
    /// Loads seed data
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
            var issueCommentService = scope.ServiceProvider.GetRequiredService<IIssueCommentService>();
            var issueService = scope.ServiceProvider.GetRequiredService<IIssueService>();
            var issueStatusService = scope.ServiceProvider.GetRequiredService<IIssueStatusService>();
            var issueTypeService = scope.ServiceProvider.GetRequiredService<IIssueTypeService>();
            var metricsTypeService = scope.ServiceProvider.GetRequiredService<IMetricsTypeService>();
            var projectComponentService = scope.ServiceProvider.GetRequiredService<IProjectComponentService>();
            var projectService = scope.ServiceProvider.GetRequiredService<IProjectService>();
            var systemTaskStatusService = scope.ServiceProvider.GetRequiredService<ISystemTaskStatusService>();
            var systemTaskTypeService = scope.ServiceProvider.GetRequiredService<ISystemTaskTypeService>();
            var systemValueTypeService = scope.ServiceProvider.GetRequiredService<ISystemValueTypeService>();
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

            var metricsTypes = await metricsTypeService.GetAllAsync();
            foreach (var metricsType in metricsTypes)
            {
                await metricsTypeService.DeleteByIdAsync(metricsType.Id);
            }

            var systemValueTypes = await systemValueTypeService.GetAllAsync();
            foreach (var systemValueType in systemValueTypes)
            {
                await systemValueTypeService.DeleteByIdAsync(systemValueType.Id);
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
            var issueCommentService = scope.ServiceProvider.GetRequiredService<IIssueCommentService>();
            var issueService = scope.ServiceProvider.GetRequiredService<IIssueService>();
            var issueStatusService = scope.ServiceProvider.GetRequiredService<IIssueStatusService>();
            var issueTypeService = scope.ServiceProvider.GetRequiredService<IIssueTypeService>();
            var metricsTypeService = scope.ServiceProvider.GetRequiredService<IMetricsTypeService>();            
            var projectComponentService = scope.ServiceProvider.GetRequiredService<IProjectComponentService>();
            var projectService = scope.ServiceProvider.GetRequiredService<IProjectService>();
            var systemTaskStatusService = scope.ServiceProvider.GetRequiredService<ISystemTaskStatusService>();
            var systemTaskTypeService = scope.ServiceProvider.GetRequiredService<ISystemTaskTypeService>();
            var systemValueTypeService = scope.ServiceProvider.GetRequiredService<ISystemValueTypeService>();
            var userService = scope.ServiceProvider.GetRequiredService<IUserService>();

            // Get seed data services
            var auditEventTypeSeed = scope.ServiceProvider.GetRequiredKeyedService<IEntityReader<AuditEventType>>("AuditEventTypeSeed");
            var issueStatusSeed = scope.ServiceProvider.GetRequiredKeyedService<IEntityReader<IssueStatus>>("IssueStatusSeed");
            var issueTypeSeed = scope.ServiceProvider.GetRequiredKeyedService<IEntityReader<IssueType>>("IssueTypeSeed");
            var metricsTypeSeed = scope.ServiceProvider.GetRequiredKeyedService<IEntityReader<MetricsType>>("MetricsTypeSeed");
            var projectComponentSeed = scope.ServiceProvider.GetRequiredKeyedService<IEntityReader<ProjectComponent>>("ProjectComponentSeed");
            var projectSeed = scope.ServiceProvider.GetRequiredKeyedService<IEntityReader<Project>>("ProjectSeed");
            var systemTaskStatusSeed = scope.ServiceProvider.GetRequiredKeyedService<IEntityReader<SystemTaskStatus>>("SystemTaskStatusSeed");
            var systemTaskTypeSeed = scope.ServiceProvider.GetRequiredKeyedService<IEntityReader<SystemTaskType>>("SystemTaskTypeSeed");
            var systemValueTypeSeed = scope.ServiceProvider.GetRequiredKeyedService<IEntityReader<SystemValueType>>("SystemValueTypeSeed");
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

            // Add audit event types
            var auditEventTypesNew = auditEventTypeSeed.Read();
            foreach (var auditEventType in auditEventTypesNew)
            {
                await auditEventTypeService.AddAsync(auditEventType);
            }

            // Get audit event types & system value types
            var auditEventTypes = await auditEventTypeService.GetAllAsync();
            var systemValueTypes = await systemValueTypeService.GetAllAsync();
            
            // Add issue statuses
            var issueStatusesNew = issueStatusSeed.Read();
            foreach (var issueStatus in issueStatusesNew)
            {
                var issueStatusResult = await issueStatusService.AddAsync(issueStatus);

                var auditEvent = new AuditEvent()
                {
                    Id = Guid.NewGuid().ToString(),
                    CreatedDateTime = DateTimeOffset.UtcNow,
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

            // Add users
            var usersNew = userSeed.Read();
            foreach (var user in usersNew)
            {
                var userResult = await userService.AddAsync(user);

                var auditEvent = new AuditEvent()
                {
                    Id = Guid.NewGuid().ToString(),
                    CreatedDateTime = DateTimeOffset.UtcNow,
                    TypeId = auditEventTypes.First(i => i.Name == AuditEventTypeNames.UserCreated).Id,
                    Parameters = new List<AuditEventParameter>()
                    {
                        new AuditEventParameter()
                        {
                            Id = Guid.NewGuid().ToString(),
                            SystemValueTypeId = systemValueTypes.First(i => i.Name == SystemValueTypeNames.UserId).Id,
                            Value = userResult.Id
                        }
                    }
                };
                await auditEventService.AddAsync(auditEvent);
            }

            // Add random issues so that we have some data
            if (randomIssuesToCreate > 0)
            {                
                var issueCreator = new RandomIssueCreator(auditEventService, auditEventTypeService,
                    issueCommentService, issueService,
                    issueStatusService, issueTypeService,
                    projectComponentService, projectService,
                    systemValueTypeService, userService);

                await issueCreator.CreateIssuesAsync(randomIssuesToCreate);
            }
        }     
    }
}
