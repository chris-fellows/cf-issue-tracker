using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;
using CFUtilities.CSV;
using System.Text;

namespace CFIssueTrackerCommon.EntityWriter
{
    public class CSVIssueWriter : IEntityWriter<Issue>
    {        
        private readonly CSVWriter<Issue> _csvWriter = new CSVWriter<Issue>();

        private readonly IIssueStatusService _issueStatusService;
        private readonly IIssueTypeService _issueTypeService;
        private readonly IProjectComponentService _projectComponentService;
        private readonly IProjectService _projectService;
        private readonly IUserService _userService;

        public CSVIssueWriter(string file, Char delimiter,
                            Encoding encoding,
                            IIssueStatusService issueStatusService,
                            IIssueTypeService issueTypeService,
                            IProjectComponentService projectComponentService,
                            IProjectService projectService,
                            IUserService userService)
        {            
            _csvWriter.Delimiter = delimiter;
            _csvWriter.Encoding = encoding;
            _csvWriter.File = file;

            _issueStatusService = issueStatusService;
            _issueTypeService = issueTypeService;   
            _projectComponentService = projectComponentService;
            _projectService = projectService;
            _userService = userService;
        }

        public void Write(IEnumerable<Issue> issues)
        {         
            var issueStatuses = _issueStatusService.GetAll();
            var issueTypes = _issueTypeService.GetAll();
            var projectComponents = _projectComponentService.GetAll();
            var projects = _projectService.GetAll();
            var users = _userService.GetAll();

            _csvWriter.AddColumn<string>("Id", i => i.Id, value => value.ToString());
            _csvWriter.AddColumn<DateTimeOffset>("CreatedDateTime", i => i.CreatedDateTime, value => value.ToString());
            _csvWriter.AddColumn<string>("CreatedUser", i => i.CreatedUserId, value => users.First(u => u.Id == value).Name);
            _csvWriter.AddColumn<string>("Description", i => i.Description, value => value.ToString());
            _csvWriter.AddColumn<string>("Status", i => i.StatusId, value => issueStatuses.First(u => u.Id == value).Name);
            _csvWriter.AddColumn<string>("Type", i => i.TypeId, value => issueTypes.First(u => u.Id == value).Name);
            _csvWriter.AddColumn<string>("Project", i => i.ProjectId, value => projects.First(u => u.Id == value).Name);
            _csvWriter.AddColumn<string>("ProjectComponent", i => i.ProjectComponentId, value => projectComponents.First(u => u.Id == value).Name);

            _csvWriter.Write(issues);
        }
    }
}
