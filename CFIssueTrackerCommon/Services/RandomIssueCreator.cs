using CFIssueTrackerCommon.Constants;
using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;

namespace CFIssueTrackerCommon.Services
{
    /// <summary>
    /// Creates random issue data
    /// </summary>
    public class RandomIssueCreator
    {
        private readonly IAuditEventService _auditEventService;
        private readonly IAuditEventTypeService _auditEventTypeService;
        private readonly IIssueCommentService _issueCommentService;
        private readonly IIssueService _issueService;
        private readonly IIssueStatusService _issueStatusService;
        private readonly IIssueTypeService _issueTypeService;
        private readonly IProjectComponentService _projectComponentService;
        private readonly IProjectService _projectService;
        private readonly ISystemValueTypeService _systemValueTypeService;
        private readonly IUserService _userService;

        public RandomIssueCreator(IAuditEventService auditEventService,
            IAuditEventTypeService auditEventTypeService,
            IIssueCommentService issueCommentService,
            IIssueService issueService,
            IIssueStatusService issueStatusService,
            IIssueTypeService issueTypeService, 
            IProjectComponentService projectComponentService,
            IProjectService projectService,
            ISystemValueTypeService systemValueTypeService,
            IUserService userService)
        {
            _auditEventService = auditEventService;
            _auditEventTypeService = auditEventTypeService;
            _issueCommentService = issueCommentService;
            _issueService = issueService;
            _issueStatusService = issueStatusService;
            _issueTypeService = issueTypeService;
            _projectComponentService = projectComponentService;
            _projectService = projectService;
            _systemValueTypeService = systemValueTypeService;
            _userService = userService;
        }

        public async Task CreateIssuesAsync(int max)
        {
            var auditEventTypes = await _auditEventTypeService.GetAllAsync();
            var issueStatuses = await _issueStatusService.GetAllAsync();
            var issueTypes = await _issueTypeService.GetAllAsync();
            var projectComponents = await _projectComponentService.GetAllAsync();
            var projects = await _projectService.GetAllAsync();
            var systemValueTypes = await _systemValueTypeService.GetAllAsync();
            var users = await _userService.GetAllAsync();

            var random = new Random();

            for (int index = 0; index < max; index++)
            {
                // Set random data                
                var issueStatus = issueStatuses[random.Next(0, issueStatuses.Count - 1)];
                var issueType = issueTypes[random.Next(0, issueTypes.Count - 1)];
                var project = projects[random.Next(0, projects.Count - 1)];
                var projectComponent = projectComponents[random.Next(0, projectComponents.Count - 1)];
               
                User? assignedUser = null;
                do
                {
                    assignedUser = users[random.Next(0, users.Count - 1)];
                } while (assignedUser.GetUserType() != Enums.UserTypes.Normal);

                User? createdUser = null;
                do
                {
                    createdUser = users[random.Next(0, users.Count - 1)];
                } while (createdUser.GetUserType() != Enums.UserTypes.Normal);

                // Create issue
                var issue = new Issue()
                {
                    Id = Guid.NewGuid().ToString(),
                    AssignedDateTime = DateTimeOffset.UtcNow,
                    AssignedUserId = assignedUser.Id,
                    CreatedDateTime = DateTimeOffset.UtcNow.Subtract(TimeSpan.FromSeconds(random.Next(0, 3600 * 24 * 7))),  // Random in last N days
                    CreatedUserId = createdUser.Id,
                    Description = "This is the issue description. It can be quite long. Or it can be quite short. It it can be both.",
                    ProjectComponentId = projectComponent.Id,
                    ProjectId = project.Id,
                    Reference = $"R{index.ToString("00000000")}",
                    StatusId = issueStatus.Id,                    
                    TypeId = issueType.Id                                       
                };

                var issueResult = await _issueService.AddAsync(issue);

                // Create audit event for issue created
                var auditEvent = new AuditEvent()
                {
                    Id = Guid.NewGuid().ToString(),
                    CreatedDateTime = DateTimeOffset.UtcNow,
                    TypeId = auditEventTypes.First(i => i.Name == AuditEventTypeNames.IssueCreated).Id,
                    Parameters = new List<AuditEventParameter>()
                    {
                        new AuditEventParameter()
                        {
                            Id = Guid.NewGuid().ToString(),
                            SystemValueTypeId = systemValueTypes.First(i => i.Name == SystemValueTypeNames.IssueId).Id,
                            Value = issueResult.Id
                        }
                    }
                };
                await _auditEventService.AddAsync(auditEvent);

                // Add random comment(s)
                var issueCount = random.Next(0, 3);
                for (int commentIndex = 0; commentIndex < issueCount; commentIndex++)
                {
                    User? commentCreatedUser = null;
                    do
                    {
                        commentCreatedUser = users[random.Next(0, users.Count - 1)];
                    } while (commentCreatedUser.GetUserType() != Enums.UserTypes.Normal);

                    var issueComment = new IssueComment()
                    {
                        Id = Guid.NewGuid().ToString(),
                        CreatedUserId = commentCreatedUser.Id,
                        CreatedDateTime = issue.CreatedDateTime.AddSeconds(random.Next(0, 3600 * 6)),
                        Description = $"Comment #{commentIndex + 1}",
                        IssueId = issue.Id                        
                    };

                    var issueCommentResult = await _issueCommentService.AddAsync(issueComment);

                    // Create audit event for issue comment created
                    var auditEventComment = new AuditEvent()
                    {
                        Id = Guid.NewGuid().ToString(),
                        CreatedDateTime = DateTimeOffset.UtcNow,
                        TypeId = auditEventTypes.First(i => i.Name == AuditEventTypeNames.IssuseCommentCreated).Id,
                        Parameters = new List<AuditEventParameter>()
                        {
                            new AuditEventParameter()
                            {
                                Id = Guid.NewGuid().ToString(),
                                SystemValueTypeId = systemValueTypes.First(i => i.Name == SystemValueTypeNames.IssueCommentId).Id,
                                Value = issueCommentResult.Id
                            }
                        }
                    };
                    await _auditEventService.AddAsync(auditEventComment);
                }
            }
        }
    }
}
