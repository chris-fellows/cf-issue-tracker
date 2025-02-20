using CFIssueTracker.Components;
using CFIssueTrackerCommon.Data;
using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Services;
using CFIssueTrackerCommon.EntityReader;
using CFIssueTrackerCommon.Models;
using CFIssueTracker.Services;
using CFIssueTrackerCommon.SystemTask;
using CFUtilities.Utilities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis.CSharp.Syntax;

const bool registerSeedDataLoad = true;
const bool registerRequestInfoService = true;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContextFactory<CFIssueTrackerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CFIssueTrackerContext") ?? throw new InvalidOperationException("Connection string 'CFIssueTrackerContext' not found.")));

// Set authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.Cookie.Name = "auth_cookie";
            options.LoginPath = "/login";
            options.Cookie.MaxAge = TimeSpan.FromMinutes(120);
            options.AccessDeniedPath = "/access-denied";            
        });
builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();
        
builder.Services.AddQuickGridEntityFrameworkAdapter();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

if (registerRequestInfoService) builder.Services.AddHttpContextAccessor();  // Added for IRequestContextService

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add request context service for current request
if (registerRequestInfoService) builder.Services.AddScoped<IRequestContextService, RequestContextService>();

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

// Add metric service for reports
builder.Services.AddScoped<IMetricService, MetricService>();

// Add seed data
if (registerSeedDataLoad)
{
    builder.Services.AddKeyedScoped<IEntityReader<AuditEventType>, AuditEventTypeSeed1>("AuditEventTypeSeed");
    builder.Services.AddKeyedScoped<IEntityReader<IssueStatus>, IssueStatusSeed1>("IssueStatusSeed");
    builder.Services.AddKeyedScoped<IEntityReader<IssueType>, IssueTypeSeed1>("IssueTypeSeed");
    builder.Services.AddKeyedScoped<IEntityReader<MetricsType>, MetricsTypeSeed1>("MetricsTypeSeed");
    builder.Services.AddKeyedScoped<IEntityReader<ProjectComponent>, ProjectComponentSeed1>("ProjectComponentSeed");
    builder.Services.AddKeyedScoped<IEntityReader<Project>, ProjectSeed1>("ProjectSeed");
    builder.Services.AddKeyedScoped<IEntityReader<User>, UserSeed1>("UserSeed");
}

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
app.UseAuthentication();    // CMF Added
app.UseAuthorization();     // CMF Added

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

/*
// Populate seed data
using (var scope = app.Services.CreateScope())
{
    new SeedLoader().LoadAsync(scope, 200).Wait();
}
*/

app.Run();
