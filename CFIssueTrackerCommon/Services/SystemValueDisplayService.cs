using CFIssueTrackerCommon.Constants;
using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;

namespace CFIssueTrackerCommon.Services
{
    public class SystemValueDisplayService : ISystemValueDisplayService
    {
        private readonly IAuditEventService _auditEventService;
        private readonly IAuditEventTypeService _auditEventTypeService;
        private readonly IIssueCommentService _issueCommentService;
        private readonly IIssueService _issueService;
        private readonly IIssueStatusService _issueStatusService;
        private readonly IIssueTypeService _issueTypeService;
        private readonly IPasswordResetService _passwordResetService;
        private readonly IProjectComponentService _projectComponentService;
        private readonly IProjectService _projectService;
        private readonly ISystemTaskTypeService _systemTaskTypeService;
        private readonly ISystemValueTypeService _systemValueTypeService;
        private readonly ITagService _tagService;
        private readonly IUserService _userService;

        public SystemValueDisplayService(IAuditEventService auditEventService,
                        IAuditEventTypeService auditEventTypeService,
                        IIssueCommentService issueCommentService,
                        IIssueService issueService,
                        IIssueStatusService issueStatusService,
                        IIssueTypeService issueTypeService,
                        IPasswordResetService passwordResetService,
                        IProjectComponentService  projectComponentService,
                        IProjectService projectService,
                        ISystemTaskTypeService systemTaskTypeService,
                        ISystemValueTypeService systemValueTypeService,
                        ITagService tagService,
                        IUserService userService)
        {
            _auditEventService = auditEventService;
            _auditEventTypeService = auditEventTypeService;
            _issueCommentService = issueCommentService;
            _issueService = issueService;
            _issueStatusService = issueStatusService;
            _issueTypeService = issueTypeService;
            _passwordResetService = passwordResetService;
            _projectComponentService = projectComponentService;
            _projectService = projectService;
            _systemTaskTypeService = systemTaskTypeService;
            _systemValueTypeService = systemValueTypeService;
            _tagService = tagService;
            _userService = userService;
        }

        public async Task<List<string[]>> GetDisplayItemsAsync(SystemValue systemValue)
        {
            // Get system value type
            var systemValueType = await _systemValueTypeService.GetByIdAsync(systemValue.TypeId);

            // Set function to get display value from value
            // TODO: Can we store the label in SystemValueType?
            var displayFunction = new Dictionary<string, Func<string, Task<List<string[]>>>>
            {
                { SystemValueTypeNames.AuditEventTypeId, async (value) =>
                    {
                        var auditEventType = await _auditEventTypeService.GetByIdAsync(value);
                        return new List<string[]>
                        {
                            new [] { "Audit Event Type", auditEventType.Name }
                        };
                    }
                },
                 { SystemValueTypeNames.IssueCommentId, async (value) =>
                    {
                        var issueComment = await _issueCommentService.GetByIdAsync(value);
                        var issue = await _issueService.GetByIdAsync(issueComment.IssueId);
                        return new List<string[]>
                        {
                            new[] { "Issue", issue.Reference },
                            new[] { "Issue Comment", issueComment.Description }
                        };
                    }
                },
                { SystemValueTypeNames.IssueId, async (value) =>
                    {
                        var issue = await _issueService.GetByIdAsync(value);
                        return new List<string[]>
                        {
                            new[] { "Issue", issue.Reference }
                        };
                    }
                },
                { SystemValueTypeNames.IssueStatusId, async (value) =>
                    {
                        var issueStatus = await _issueStatusService.GetByIdAsync(value);
                        return new List<string[]>
                        {
                            new[] { "Issue Status", issueStatus.Name }
                        };
                    }
                },
                { SystemValueTypeNames.IssueTypeId, async (value) =>
                    {
                        var issueType = await _issueTypeService.GetByIdAsync(value);
                        return new List<string[]>
                        {
                            new[] { "Issue Type", issueType.Name }
                        };
                    }
                },
                { SystemValueTypeNames.PasswordResetId, async (value) =>
                    {
                        var passwordReset = await _passwordResetService.GetByIdAsync(value);
                        var user = await _userService.GetByIdAsync(passwordReset.UserId);
                        return new List<string[]>
                        {
                            new[] { "Password reset URL", passwordReset.Url },
                            new[] { "User", user.Name }
                        };
                    }
                },
                { SystemValueTypeNames.ProjectComponentId, async (value) =>
                    {
                        var projectComponent = await _projectComponentService.GetByIdAsync(value);
                        var project = await _projectService.GetByIdAsync(projectComponent.ProjectId);
                        return new List<string[]>()
                        {
                            new[] { "Project", project.Name },
                            new[] { "Project Component", projectComponent.Name }
                        };
                    }
                },
                 { SystemValueTypeNames.ProjectId, async (value) =>
                    {
                        var project = await _projectService.GetByIdAsync(value);
                        return new List<string[]>()
                        {
                            new[] { "Project", project.Name }
                        };
                    }
                },
                { SystemValueTypeNames.SystemTaskTypeId, async (value) =>
                    {
                        var systemTaskType = await _systemTaskTypeService.GetByIdAsync(value);
                        return new List<string[]>()
                        {
                            new[] { "System Task", systemTaskType.Name }
                        };
                    }
                },
                { SystemValueTypeNames.SystemValueTypeId, async (value) =>
                    {
                        var svt = await _systemValueTypeService.GetByIdAsync(value);
                        return new List<string[]>()
                        {
                            new[] { "System Value Type", svt.Name }
                        };
                    }
                },
                { SystemValueTypeNames.UserId, async (value) =>
                    {
                        var user = await _userService.GetByIdAsync(value);
                        return new List<string[]>()
                        {
                            new[] { "User", user.Name }
                        };
                    }
                }
            };

            return displayFunction.ContainsKey(systemValueType.Name) ?
                        await displayFunction[systemValueType.Name](systemValue.Value) :
                        new List<string[]> {
                            new[] { systemValueType.Name, systemValue.Value }
                        };
        }
    }        
}
