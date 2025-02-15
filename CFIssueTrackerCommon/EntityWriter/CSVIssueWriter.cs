using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;
using System.Text;

namespace CFIssueTrackerCommon.EntityWriter
{
    public class CSVIssueWriter : IEntityWriter<Issue>
    {
        private readonly string _file;
        private readonly Char _delimiter;

        private readonly IIssueStatusService _issueStatusService;
        private readonly IIssueTypeService _issueTypeService;
        private readonly IProjectComponentService _projectComponentService;
        private readonly IProjectService _projectService;
        private readonly IUserService _userService;

        public CSVIssueWriter(string file, Char delimiter,
                            IIssueStatusService issueStatusService,
                            IIssueTypeService issueTypeService,
                            IProjectComponentService projectComponentService,
                            IProjectService projectService,
                            IUserService userService)
        {
            _file = file;
            _delimiter = delimiter;

            _issueStatusService = issueStatusService;
            _issueTypeService = issueTypeService;   
            _projectComponentService = projectComponentService;
            _projectService = projectService;
            _userService = userService;
        }

        public void Write(IEnumerable<Issue> issues)
        {
            if (File.Exists(_file))
            {
                File.Delete(_file);
            }

            var issueStatuses = _issueStatusService.GetAll();
            var issueTypes = _issueTypeService.GetAll();
            var projectComponents = _projectComponentService.GetAll();
            var projects = _projectService.GetAll();
            var users = _userService.GetAll();

            using (var streamWriter = new StreamWriter(_file, true, Encoding.UTF8))
            {
                streamWriter.WriteLine($"Id{_delimiter}CreatedDateTime{_delimiter}CreatedUser{_delimiter}Status{_delimiter}Type{_delimiter}Project{_delimiter}Component");

                foreach (var issue in issues)
                {
                    Write(issue, streamWriter, issueStatuses, issueTypes, projectComponents, projects, users);
                }
            }
        }

        private void Write(Issue issue, 
                        StreamWriter streamWriter,
                        IReadOnlyList<IssueStatus> issueStatuses,
                        IReadOnlyList<IssueType> issueTypes,
                        IReadOnlyList<ProjectComponent> projectComponents,
                        IReadOnlyList<Project> projects,
                        IReadOnlyList<User> users)
        {
            var createdUser = users.First(u => u.Id == issue.CreatedUserId);
            var assignedUser = users.First(u => u.Id == issue.AssignedUserId);
            var issueStatus = issueStatuses.First(i => i.Id == issue.StatusId);
            var issueType = issueTypes.First(i => i.Id == issue.TypeId);
            var project = projects.First(p => p.Id == issue.ProjectId);
            var projectComponent = projectComponents.First(pc => pc.Id == issue.ProjectComponentId);

            streamWriter.WriteLine($"{issue.Id}{_delimiter}{issue.CreatedDateTime}{_delimiter}" +
                $"{createdUser.Name}{_delimiter}{issueStatus.Name}{_delimiter}" +
                $"{issueType.Name}{_delimiter}{project.Name}{_delimiter}" +
                $"{projectComponent.Name}");
        }
    }
}
