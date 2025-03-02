using CFIssueTrackerCommon.Constants;
using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CFIssueTrackerCommon.Services
{
    /// <summary>
    /// Creates random issue data
    /// </summary>
    public class RandomIssueCreator
    {
        private readonly IAuditEventService _auditEventService;
        private readonly IAuditEventTypeService _auditEventTypeService;
        private readonly IDocumentService _documentService;
        private readonly IIssueCommentService _issueCommentService;
        private readonly IIssueService _issueService;
        private readonly IIssueStatusService _issueStatusService;
        private readonly IIssueTypeService _issueTypeService;
        private readonly IProjectComponentService _projectComponentService;
        private readonly IProjectService _projectService;
        private readonly ISystemValueTypeService _systemValueTypeService;
        private readonly ITagService _tagService;
        private readonly IUserService _userService;

        public RandomIssueCreator(IAuditEventService auditEventService,
            IAuditEventTypeService auditEventTypeService,
            IDocumentService documentService,
            IIssueCommentService issueCommentService,
            IIssueService issueService,
            IIssueStatusService issueStatusService,
            IIssueTypeService issueTypeService, 
            IProjectComponentService projectComponentService,
            IProjectService projectService,
            ISystemValueTypeService systemValueTypeService,
            ITagService tagService,
            IUserService userService)
        {
            _auditEventService = auditEventService;
            _auditEventTypeService = auditEventTypeService;
            _documentService = documentService;
            _issueCommentService = issueCommentService;
            _issueService = issueService;
            _issueStatusService = issueStatusService;
            _issueTypeService = issueTypeService;
            _projectComponentService = projectComponentService;
            _projectService = projectService;
            _systemValueTypeService = systemValueTypeService;
            _tagService = tagService;
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
            var tags = await _tagService.GetAllAsync();
            var users = await _userService.GetAllAsync();

            var systemUser = users.First(u => u.GetUserType() == Enums.UserTypes.System);

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

                // Add random tags
                var issueTagRefs = new List<TagReference>();
                if (tags.Any() && random.Next(0, 100) >= 50)     // Approx percentage of issues to add for
                {
                    var countTagsToAdd = random.Next(0, 2);
                    for (int tagIndex =0; tagIndex < countTagsToAdd; tagIndex++)
                    {
                        var tag = tags[random.Next(0, tags.Count - 1)];
                        if (!issueTagRefs.Any(t => t.TagId == tag.Id))   // Only add tag once
                        {
                            issueTagRefs.Add(new TagReference()
                            {
                                Id = Guid.NewGuid().ToString(),
                                TagId = tag.Id
                            });
                        }
                    }
                }

                // Add tracking user for some issues
                var trackingUsersRefs = new List<UserReference>();
                if (random.Next(0, 100) >= 50)       // Approx percentage of issues to add for
                {
                    var trackingUser = users[random.Next(0, users.Count - 1)];
                    trackingUsersRefs.Add(new UserReference()
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserId = trackingUser.Id
                    });
                }

                // Add document for some issues
                var documentRefs =new List<DocumentReference>();
                if (random.Next(0, 100) >= 90)  // Approx percentage of issues to add for
                {
                    // Add document
                    var document = new Document()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Test Document 1.txt",
                        Content = Encoding.UTF8.GetBytes("This is some text")
                    };
                    await _documentService.AddAsync(document);

                    documentRefs.Add(new DocumentReference()
                    {
                        Id = Guid.NewGuid().ToString(),
                        DocumentId = document.Id
                    });
                }

                // Create issue
                var issue = new Issue()
                {
                    Id = Guid.NewGuid().ToString(),
                    AssignedDateTime = DateTimeOffset.UtcNow,
                    AssignedUserId = assignedUser.Id,
                    CreatedDateTime = DateTimeOffset.UtcNow.Subtract(TimeSpan.FromSeconds(random.Next(0, 3600 * 24 * 7))),  // Random in last N days
                    CreatedUserId = createdUser.Id,
                    Description = "This is the issue description. It can be quite long. Or it can be quite short. It it can be both.",
                    Documents = documentRefs,
                    ProjectComponentId = projectComponent.Id,
                    ProjectId = project.Id,
                    Reference = $"R{index.ToString("00000000")}",
                    StatusId = issueStatus.Id,                    
                    Tags = issueTagRefs,
                    TrackingUsers = trackingUsersRefs,
                    TypeId = issueType.Id                                
                };

                var issueResult = await _issueService.AddAsync(issue);

                // Create audit event for issue created
                var auditEvent = new AuditEvent()
                {
                    Id = Guid.NewGuid().ToString(),
                    CreatedDateTime = DateTimeOffset.UtcNow,
                    CreatedUserId = systemUser.Id,
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
                        CreatedUserId = systemUser.Id,
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
