using CFIssueTracker.Components;
using CFIssueTracker.Data;
using CFIssueTracker.Extensions;
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
using Microsoft.Extensions.Options;
using CFIssueTrackerCommon.Constants;

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

//builder.Services.AddAntiforgery(opts => opts.Cookie.Name = "___RequestVerificationToken");      // Manually added

builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();
        
builder.Services.AddQuickGridEntityFrameworkAdapter();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

if (registerRequestInfoService) builder.Services.AddHttpContextAccessor();  // Added for IRequestContextService

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add password service
builder.Services.AddScoped<IPasswordService, PBKDF2PasswordService>();

// Add toast service
builder.Services.AddSingleton<IToastService, ToastService>();

// Add email config from appsettings.json
builder.Services.Configure<EmailConfig>(builder.Configuration.GetSection(nameof(EmailConfig)));
builder.Services.AddSingleton<IEmailConfig>(sp => sp.GetRequiredService<IOptions<EmailConfig>>().Value);

// Add request context service for current request
if (registerRequestInfoService) builder.Services.AddScoped<IRequestContextService, RequestContextService>();

// Add data services
builder.Services.AddScoped<IAuditEventService, EFAuditEventService>();
builder.Services.AddScoped<IAuditEventTypeService, EFAuditEventTypeService>();
builder.Services.AddScoped<IContentTemplateService, EFContentTemplateService>();
builder.Services.AddScoped<IDocumentService, EFDocumentService>();
builder.Services.AddScoped<IIssueCommentService, EFIssueCommentService>();
builder.Services.AddScoped<IIssueService, EFIssueService>();
builder.Services.AddScoped<IIssueStatusService, EFIssueStatusService>();
builder.Services.AddScoped<IIssueTypeService, EFIssueTypeService>();
builder.Services.AddScoped<IMetricsTypeService, EFMetricsTypeService>();
builder.Services.AddScoped<INotificationGroupService, EFNotificationGroupService>();
builder.Services.AddScoped<IPasswordResetService, EFPasswordResetService>();
builder.Services.AddScoped<IProjectComponentService, EFProjectComponentService>();
builder.Services.AddScoped<IProjectService, EFProjectService>();
builder.Services.AddScoped<ISystemTaskJobService, EFSystemTaskJobService>();
builder.Services.AddScoped<ISystemTaskStatusService, EFSystemTaskStatusService>();
builder.Services.AddScoped<ISystemTaskTypeService, EFSystemTaskTypeService>();
builder.Services.AddScoped<ISystemValueTypeService, EFSystemValueTypeService>();
builder.Services.AddScoped<ITagService, EFTagService>();
builder.Services.AddScoped<IUserService, EFUserService>();

// Add system value display service
builder.Services.AddScoped<ISystemValueDisplayService, SystemValueDisplayService>();

// Add audit event processor (E.g. Create notifications)
builder.Services.AddScoped<IAuditEventProcessorService, AuditEventProcessorService>();

// Add file security checker (E.g. Prevent potentially malicious image files being uploaded)
builder.Services.AddScoped<IFileSecurityCheckerService, FileSecurityCheckerService>();

// Add email services
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.RegisterAllTypes<IEmailCreator>(new[] { typeof(Program).Assembly, typeof(ISystemTask).Assembly });

// Add Datadog services
builder.Services.AddScoped<IDatadogService, DatadogService>();
builder.Services.RegisterAllTypes<IDatadogCreator>(new[] { typeof(Program).Assembly, typeof(ISystemTask).Assembly });

// Add Slack services
builder.Services.AddScoped<ISlackService, SlackService>();
builder.Services.RegisterAllTypes<ISlackCreator>(new[] { typeof(Program).Assembly, typeof(ISystemTask).Assembly });

// Add Teams services
builder.Services.AddScoped<ITeamsService, TeamsService>();
builder.Services.RegisterAllTypes<ITeamsCreator>(new[] { typeof(Program).Assembly, typeof(ISystemTask).Assembly });

// Add metric service for reports
builder.Services.AddScoped<IAuditEventMetricService, AuditEventMetricService>();
builder.Services.AddScoped<IIssueMetricService, IssueMetricService>();

// Register system tasks. Will only use the ones that there's a config for
builder.Services.RegisterAllTypes<ISystemTask>(new[] { typeof(Program).Assembly, typeof(ISystemTask).Assembly });

// Add seed data. Only need it as a one-off
if (registerSeedDataLoad)
{
    builder.Services.AddKeyedScoped<IEntityReader<AuditEventType>, AuditEventTypeSeed1>("AuditEventTypeSeed");
    builder.Services.AddKeyedScoped<IEntityReader<ContentTemplate>, ContentTemplateSeed1>("ContentTemplateSeed");
    builder.Services.AddKeyedScoped<IEntityReader<IssueStatus>, IssueStatusSeed1>("IssueStatusSeed");
    builder.Services.AddKeyedScoped<IEntityReader<IssueType>, IssueTypeSeed1>("IssueTypeSeed");
    builder.Services.AddKeyedScoped<IEntityReader<MetricsType>, MetricsTypeSeed1>("MetricsTypeSeed");
    builder.Services.AddKeyedScoped<IEntityReader<NotificationGroup>, NotificationGroupSeed1>("NotificationGroupSeed");
    builder.Services.AddKeyedScoped<IEntityReader<ProjectComponent>, ProjectComponentSeed1>("ProjectComponentSeed");
    builder.Services.AddKeyedScoped<IEntityReader<Project>, ProjectSeed1>("ProjectSeed");
    builder.Services.AddKeyedScoped<IEntityReader<SystemTaskStatus>, SystemTaskStatusSeed1>("SystemTaskStatusSeed");
    builder.Services.AddKeyedScoped<IEntityReader<SystemTaskType>, SystemTaskTypeSeed1>("SystemTaskTypeSeed");
    builder.Services.AddKeyedScoped<IEntityReader<SystemValueType>, SystemValueTypeSeed1>("SystemValueTypeSeed");
    builder.Services.AddKeyedScoped<IEntityReader<Tag>, TagSeed1>("TagSeed");
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
            SystemTaskName = SystemTaskTypeNames.ManagePasswordResets,
            ExecuteFrequency = TimeSpan.FromMinutes(30)
        },
        new SystemTaskConfig()
        {
            SystemTaskName = SystemTaskTypeNames.ManageSystemTaskJobs,
            ExecuteFrequency = TimeSpan.FromHours(12),
        },
        new SystemTaskConfig()
        {
            SystemTaskName = SystemTaskTypeNames.SendDatadog,
            ExecuteFrequency = TimeSpan.FromMinutes(15)
        },
        new SystemTaskConfig()
        {
            SystemTaskName = SystemTaskTypeNames.SendEmail,
            ExecuteFrequency = TimeSpan.FromMinutes(15)
        },
        new SystemTaskConfig()
        {
            SystemTaskName = SystemTaskTypeNames.SendSlack,
            ExecuteFrequency = TimeSpan.FromMinutes(15)
        },
        new SystemTaskConfig()
        {
            SystemTaskName = SystemTaskTypeNames.SendTeams,
            ExecuteFrequency = TimeSpan.FromMinutes(15)
        }
    };
    systemTaskConfigs.ForEach(stc => stc.NextExecuteTime = DateTimeUtilities.GetNextTaskExecuteTimeFromFrequency(stc.ExecuteFrequency));

    return new SystemTaskList(5, systemTaskConfigs);
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
//app.UseAntiforgery();
app.UseAuthentication();    // CMF Added
app.UseAuthorization();     // CMF Added

app.UseAntiforgery();   // CF Moved from above

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

using (var scope = app.Services.CreateScope())
{
    // Check for data
    var systemValueTypeService = scope.ServiceProvider.GetRequiredService<ISystemValueTypeService>();
    var systemValuesTypes = systemValueTypeService.GetAll();
    if (!systemValuesTypes.Any())
    {
        throw new ArgumentException("System contains no data");
    }

    // Enable this to load seed data
    //new SeedLoader().DeleteAsync(scope).Wait();
    //new SeedLoader().LoadAsync(scope, 200).Wait();
}

app.Run();
