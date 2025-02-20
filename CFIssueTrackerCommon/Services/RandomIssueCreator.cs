using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;

namespace CFIssueTrackerCommon.Services
{
    public class RandomIssueCreator
    {
        private readonly IIssueCommentService _issueCommentService;
        private readonly IIssueService _issueService;
        private readonly IIssueStatusService _issueStatusService;
        private readonly IIssueTypeService _issueTypeService;
        private readonly IProjectComponentService _projectComponentService;
        private readonly IProjectService _projectService;
        private readonly IUserService _userService;

        public RandomIssueCreator(IIssueCommentService issueCommentService,
            IIssueService issueService,
            IIssueStatusService issueStatusService,
            IIssueTypeService issueTypeService, 
            IProjectComponentService projectComponentService,
            IProjectService projectService,
            IUserService userService)
        {
            _issueCommentService = issueCommentService;
            _issueService = issueService;
            _issueStatusService = issueStatusService;
            _issueTypeService = issueTypeService;
            _projectComponentService = projectComponentService;
            _projectService = projectService;
            _userService = userService;
        }

        public async Task CreateIssuesAsync(int max)
        {
            var issueStatuses = await _issueStatusService.GetAllAsync();
            var issueTypes = await _issueTypeService.GetAllAsync();
            var projectComponents = await _projectComponentService.GetAllAsync();
            var projects = await _projectService.GetAllAsync();
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

                await _issueService.AddAsync(issue);

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

                    await _issueCommentService.AddAsync(issueComment);
                }
            }
        }
    }
}
