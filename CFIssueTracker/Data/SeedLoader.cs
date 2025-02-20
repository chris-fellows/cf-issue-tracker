using CFIssueTrackerCommon.EntityReader;
using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;
using CFIssueTrackerCommon.Services;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CFIssueTracker.Data
{
    /// <summary>
    /// Loads seed data
    /// </summary>
    public class SeedLoader
    {
        public async Task LoadAsync(IServiceScope scope, int randomIssuesToCreate)
        {
            // Get services
            var auditEventTypeService = scope.ServiceProvider.GetService<IAuditEventTypeService>();
            var issueCommentService = scope.ServiceProvider.GetRequiredService<IIssueCommentService>();
            var issueService = scope.ServiceProvider.GetRequiredService<IIssueService>();
            var issueStatusService = scope.ServiceProvider.GetRequiredService<IIssueStatusService>();
            var issueTypeService = scope.ServiceProvider.GetRequiredService<IIssueTypeService>();
            var metricsTypeService = scope.ServiceProvider.GetRequiredService<IMetricsTypeService>();            
            var projectComponentService = scope.ServiceProvider.GetRequiredService<IProjectComponentService>();
            var projectService = scope.ServiceProvider.GetRequiredService<IProjectService>();
            var userService = scope.ServiceProvider.GetRequiredService<IUserService>();

            // Get seed data services
            var auditEventTypeSeed = scope.ServiceProvider.GetRequiredKeyedService<IEntityReader<AuditEventType>>("AuditEventTypeSeed");
            var issueStatusSeed = scope.ServiceProvider.GetRequiredKeyedService<IEntityReader<IssueStatus>>("IssueStatusSeed");
            var issueTypeSeed = scope.ServiceProvider.GetRequiredKeyedService<IEntityReader<IssueType>>("IssueTypeSeed");
            var metricsTypeSeed = scope.ServiceProvider.GetRequiredKeyedService<IEntityReader<MetricsType>>("MetricsTypeSeed");
            var projectComponentSeed = scope.ServiceProvider.GetRequiredKeyedService<IEntityReader<ProjectComponent>>("ProjectComponentSeed");
            var projectSeed = scope.ServiceProvider.GetRequiredKeyedService<IEntityReader<Project>>("ProjectSeed");
            var userSeed = scope.ServiceProvider.GetRequiredKeyedService<IEntityReader<User>>("UserSeed");

            // Check that no data exists
            var auditEventTypesOld = await auditEventTypeService.GetAllAsync();
            if (auditEventTypesOld.Any())
            {
                throw new ArgumentException("Cannot load seed data because data already exists");
            }

            var auditEventTypesNew = auditEventTypeSeed.Read();
            foreach (var auditEventType in auditEventTypesNew)
            {
                await auditEventTypeService.AddAsync(auditEventType);
            }

            var issueStatusesNew = issueStatusSeed.Read();
            foreach (var issueStatus in issueStatusesNew)
            {
                await issueStatusService.AddAsync(issueStatus);
            }

            var issueTypesNew = issueTypeSeed.Read();
            foreach (var issueType in issueTypesNew)
            {
                await issueTypeService.AddAsync(issueType);
            }

            var metricsTypes = metricsTypeSeed.Read();
            foreach (var metricsType in metricsTypes)
            {
                await metricsTypeService.AddAsync(metricsType);
            }

            var projectsNew = projectSeed.Read();
            foreach (var project in projectsNew)
            {
                await projectService.AddAsync(project);
            }

            var projectComponentsNew = projectComponentSeed.Read();
            foreach (var projectComponent in projectComponentsNew)
            {
                projectComponent.ProjectId = projectsNew.ToList()[0].Id;
                await projectComponentService.AddAsync(projectComponent);
            }

            var usersNew = userSeed.Read();
            foreach (var user in usersNew)
            {
                await userService.AddAsync(user);
            }

            // Create random issues
            if (randomIssuesToCreate > 0)
            {                
                var issueCreator = new RandomIssueCreator(issueCommentService, issueService,
                    issueStatusService, issueTypeService,
                    projectComponentService, projectService, userService);

                await issueCreator.CreateIssuesAsync(randomIssuesToCreate);
            }
        }
    }
}
