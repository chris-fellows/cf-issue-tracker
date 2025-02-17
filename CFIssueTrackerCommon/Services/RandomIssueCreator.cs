using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;

namespace CFIssueTrackerCommon.Services
{
    public class RandomIssueCreator
    {
        private readonly IIssueService _issueService;
        private readonly IIssueStatusService _issueStatusService;
        private readonly IIssueTypeService _issueTypeService;
        private readonly IProjectComponentService _projectComponentService;
        private readonly IProjectService _projectService;
        private readonly IUserService _userService;

        public  RandomIssueCreator(IIssueService issueService,
            IIssueStatusService issueStatusService,
            IIssueTypeService issueTypeService, 
            IProjectComponentService projectComponentService,
            IProjectService projectService,
            IUserService userService)
        {
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
                var assignedUser = users[random.Next(0, users.Count - 1)];
                var createdUser = users[random.Next(0, users.Count - 1)];
                var issueStatus = issueStatuses[random.Next(0, issueStatuses.Count - 1)];
                var issueType = issueTypes[random.Next(0, issueTypes.Count - 1)];
                var project = projects[random.Next(0, projects.Count - 1)];
                var projectComponent = projectComponents[random.Next(0, projectComponents.Count - 1)];

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
            }
        }
    }
}
