using CFIssueTracker.Components;
using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CFIssueTracker.Data;
using CFIssueTrackerCommon.EntityReader;
using CFIssueTrackerCommon.Models;
using CFIssueTracker.Services;
using CFIssueTrackerCommon.SystemTask;
using CFUtilities.Utilities;
//using CFIssueTrackerCommon.EntityWriter;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContextFactory<CFIssueTrackerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CFIssueTrackerContext") ?? throw new InvalidOperationException("Connection string 'CFIssueTrackerContext' not found.")));

builder.Services.AddQuickGridEntityFrameworkAdapter();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add request context service for current request
builder.Services.AddScoped<IRequestContextService, RequestContextService>();

// Add data services
builder.Services.AddScoped<IAuditEventService, EFAuditEventService>();
builder.Services.AddScoped<IAuditEventTypeService, EFAuditEventTypeService>();
builder.Services.AddScoped<IIssueCommentService, EFIssueCommentService>();
builder.Services.AddScoped<IIssueService, EFIssueService>();
builder.Services.AddScoped<IIssueStatusService, EFIssueStatusService>();
builder.Services.AddScoped<IIssueTypeService, EFIssueTypeService>();
builder.Services.AddScoped<IMetricsTypeService, EFMetricsTypeService>();
builder.Services.AddScoped<IProjectComponentService, EFProjectComponentService>();
builder.Services.AddScoped<IProjectService, EFProjectService>();
builder.Services.AddScoped<IUserService, EFUserService>();

builder.Services.AddScoped<IMetricService, MetricService>();

// Add seed data
builder.Services.AddKeyedScoped<IEntityReader<AuditEventType>, AuditEventTypeSeed1>("AuditEventTypeSeed");
builder.Services.AddKeyedScoped<IEntityReader<IssueStatus>, IssueStatusSeed1>("IssueStatusSeed");
builder.Services.AddKeyedScoped<IEntityReader<IssueType>, IssueTypeSeed1>("IssueTypeSeed");
builder.Services.AddKeyedScoped<IEntityReader<MetricsType>, MetricsTypeSeed1>("MetricsTypeSeed");
builder.Services.AddKeyedScoped<IEntityReader<ProjectComponent>, ProjectComponentSeed1>("ProjectComponentSeed");
builder.Services.AddKeyedScoped<IEntityReader<Project>, ProjectSeed1>("ProjectSeed");
builder.Services.AddKeyedScoped<IEntityReader<User>, UserSeed1>("UserSeed");

// Can't be resolved using string constructor parameter
//// Add CSV writers
//builder.Services.AddKeyedScoped<IEntityWriter<AuditEvent>, CSVAuditEventWriter>("CSVAuditEventType");
//builder.Services.AddKeyedScoped<IEntityWriter<AuditEventType>, CSVAuditEventTypeWriter>("CSVAuditEventType");
//builder.Services.AddKeyedScoped<IEntityWriter<IssueStatus>, CSVIssueStatusWriter>("CSVIssueStatus");
//builder.Services.AddKeyedScoped<IEntityWriter<IssueType>, CSVIssueTypeWriter>("CSVIssueType");
//builder.Services.AddKeyedScoped<IEntityWriter<ProjectComponent>, CSVProjectComponentWriter>("CSVProjectComponent");
//builder.Services.AddKeyedScoped<IEntityWriter<Project>, CSVProjectWriter>("CSVProject");
//builder.Services.AddKeyedScoped<IEntityWriter<User>, CSVUserWriter>("CSVUser");

builder.Services.AddSingleton<ICache, MemoryCache>();

// Set system task list
builder.Services.AddSingleton<ISystemTaskList>((scope) =>
{
    // Set system task configs
    var systemTaskConfigs = new List<SystemTaskConfig>()
    {
        new SystemTaskConfig()
        {
            SystemTaskName = NotificationSystemTask.TaskName,
            ExecuteFrequency = TimeSpan.FromMinutes(5)        
        }
    };
    systemTaskConfigs.ForEach(stc => stc.NextExecuteTime = DateTimeUtilities.GetNextTaskExecuteTimeFromFrequency(stc.ExecuteFrequency));

    // Set system tasks
    var systemTasks = new List<ISystemTask>()
    {
        new NotificationSystemTask()
    };

    return new SystemTaskList(5, systemTasks, systemTaskConfigs);
});

// Add background service for system tasks
builder.Services.AddHostedService<SystemTaskBackgroundService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseMigrationsEndPoint();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

//// Populate seed data
//using (var scope = app.Services.CreateScope())
//{
//    // Get services    
//    var issueCommentService =scope.ServiceProvider.GetRequiredService<IIssueCommentService>();
//    var issueService = scope.ServiceProvider.GetRequiredService<IIssueService>();

//    // Get all issues
//    var issues = issueService.GetAll();

//    // Add comments for each issue
//    foreach(var issue in issues)
//    {
//        var issueComments = new List<IssueComment>()
//        {
//            new IssueComment()
//            {
//                Id = Guid.NewGuid().ToString(),
//                CreatedDateTime = DateTimeOffset.UtcNow,
//                Description= "Comment #1",
//                IssueId = issue.Id
//            },
//            new IssueComment()
//            {
//                Id = Guid.NewGuid().ToString(),
//                CreatedDateTime = DateTimeOffset.UtcNow,
//                Description= "Comment #2",
//                IssueId = issue.Id
//            }
//        };

//        foreach(var issueComment in issueComments)
//        {
//            issueCommentService.AddAsync(issueComment).Wait();
//        }
//    }

//    int xxx = 1000;
//}

// Populate seed data
//using (var scope = app.Services.CreateScope())
//{
//    // Get services
//    var auditEventTypeService = scope.ServiceProvider.GetService<IAuditEventTypeService>();
//    var issueService = scope.ServiceProvider.GetRequiredService<IIssueService>();
//    var issueStatusService = scope.ServiceProvider.GetRequiredService<IIssueStatusService>();
//    var issueTypeService = scope.ServiceProvider.GetRequiredService<IIssueTypeService>();
//    var metricsTypeService = scope.ServiceProvider.GetRequiredService<IMetricsTypeService>();
//    var projectComponentService = scope.ServiceProvider.GetRequiredService<IProjectComponentService>();
//    var projectService = scope.ServiceProvider.GetRequiredService<IProjectService>();
//    var userService = scope.ServiceProvider.GetRequiredService<IUserService>();

//    // Get seed data services
//    var auditEventTypeSeed = scope.ServiceProvider.GetRequiredKeyedService<IEntityReader<AuditEventType>>("AuditEventTypeSeed");
//    var issueStatusSeed = scope.ServiceProvider.GetRequiredKeyedService<IEntityReader<IssueStatus>>("IssueStatusSeed");
//    var issueTypeSeed = scope.ServiceProvider.GetRequiredKeyedService<IEntityReader<IssueType>>("IssueTypeSeed");
//    var metricsTypeSeed = scope.ServiceProvider.GetRequiredKeyedService<IEntityReader<MetricsType>>("MetricsTypeSeed");
//    var projectComponentSeed = scope.ServiceProvider.GetRequiredKeyedService<IEntityReader<ProjectComponent>>("ProjectComponentSeed");
//    var projectSeed = scope.ServiceProvider.GetRequiredKeyedService<IEntityReader<Project>>("ProjectSeed");
//    var userSeed = scope.ServiceProvider.GetRequiredKeyedService<IEntityReader<User>>("UserSeed");

//    var auditEventTypesNew = auditEventTypeSeed.Read();
//    foreach (var auditEventType in auditEventTypesNew)
//    {
//        auditEventTypeService.AddAsync(auditEventType).Wait();
//    }

//    var issueStatusesNew = issueStatusSeed.Read();
//    foreach (var issueStatus in issueStatusesNew)
//    {
//        issueStatusService.AddAsync(issueStatus).Wait();
//    }

//    var issueTypesNew = issueTypeSeed.Read();
//    foreach (var issueType in issueTypesNew)
//    {
//        issueTypeService.AddAsync(issueType).Wait();
//    }

//    var metricsTypes = metricsTypeSeed.Read();
//    foreach (var metricsType in metricsTypes)
//    {
//        metricsTypeService.AddAsync(metricsType).Wait();
//    }

//    var projectsNew = projectSeed.Read();
//    foreach (var project in projectsNew)
//    {
//        projectService.AddAsync(project).Wait();
//    }

//    var projectComponentsNew = projectComponentSeed.Read();
//    foreach (var projectComponent in projectComponentsNew)
//    {
//        projectComponent.ProjectId = projectsNew.ToList()[0].Id;
//        projectComponentService.AddAsync(projectComponent).Wait();
//    }

//    var usersNew = userSeed.Read();
//    foreach (var user in usersNew)
//    {
//        userService.AddAsync(user).Wait();
//    }

//    // Create issues
//    var issueCreator = new RandomIssueCreator(issueService, issueStatusService, issueTypeService,
//        projectComponentService, projectService, userService);

//    issueCreator.CreateIssuesAsync(200).Wait();
//}

app.Run();
