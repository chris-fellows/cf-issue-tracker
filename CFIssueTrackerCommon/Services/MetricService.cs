using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;

namespace CFIssueTrackerCommon.Services
{
    public class MetricService : IMetricService
    {
        private readonly IIssueService _issueService;
        private readonly IIssueStatusService _issueStatusService;
        private readonly IIssueTypeService _issueTypeService;
        private readonly IProjectService _projectService;
        private readonly IUserService _userService;

        public MetricService(IIssueService issueService,
            IIssueStatusService issueStatusService,
            IIssueTypeService issueTypeService,
            IProjectService projectService,
            IUserService userService)
        {
            _issueService = issueService;
            _issueStatusService = issueStatusService;
            _issueTypeService = issueTypeService;
            _projectService = projectService;
            _userService = userService;
        }

        public Metrics GetIssueCountByProjectMetrics()
        {
            var issues = _issueService.GetAll();
            var projects = _projectService.GetAll();

            var metrics = new Metrics() { Title = "Issues By Project" };

            foreach (var project in projects.OrderBy(p => p.Name))
            {
                var issuesFound = issues.Where(i => i.ProjectId == project.Id);

                var metric = new Metric()
                {
                    Value = issuesFound.Count(),
                    Dimensions = new List<string>()
                    {
                        project.Name
                    }
                };
                metrics.Items.Add(metric);
            }

            return metrics;
        }

        public Metrics GetIssueCountByStatusMetrics()
        {
            var issues = _issueService.GetAll();
            var issueStatuses = _issueStatusService.GetAll();

            var metrics = new Metrics() { Title = "Issues By Status" };

            foreach (var issueStatus in issueStatuses.OrderBy(i => i.Name))
            {
                var issuesFound = issues.Where(i => i.StatusId == issueStatus.Id);

                var metric = new Metric()
                {
                    Value = issuesFound.Count(),
                    Dimensions = new List<string>()
                    {
                        issueStatus.Name
                    }
                };
                metrics.Items.Add(metric);
            }

            return metrics;
        }

        public Metrics GetIssueCountByTypeMetrics()
        {
            var issues = _issueService.GetAll();
            var issueTypes = _issueTypeService.GetAll();

            var metrics = new Metrics() { Title = "Issues By Type" };

            foreach (var issueType in issueTypes.OrderBy(i => i.Name))
            {
                var issuesFound = issues.Where(i => i.TypeId == issueType.Id);

                var metric = new Metric()
                {
                    Value = issuesFound.Count(),
                    Dimensions = new List<string>()
                    {
                        issueType.Name
                    }
                };
                metrics.Items.Add(metric);
            }

            return metrics;
        }

        public Metrics GetIssueCountByCreatedUserMetrics()
        {
            var issues = _issueService.GetAll();
            var users = _userService.GetAll();

            var metrics = new Metrics() { Title = "Issues By Created User" };

            foreach (var user in users.OrderBy(u => u.Name))
            {
                var issuesFound = issues.Where(i => i.CreatedUserId == user.Id);

                var metric = new Metric()
                {
                    Value = issuesFound.Count(),
                    Dimensions = new List<string>()
                    {
                        user.Name
                    }
                };
                metrics.Items.Add(metric);
            }

            return metrics;
        }

        public Metrics GetIssueCountByAssignedUserMetrics()
        {
            var issues = _issueService.GetAll();
            var users = _userService.GetAll();

            var metrics = new Metrics() { Title = "Issues By Assigned User" };

            foreach (var user in users.OrderBy(u => u.Name))
            {
                var issuesFound = issues.Where(i => i.AssignedUserId == user.Id);

                var metric = new Metric()
                {
                    Value = issuesFound.Count(),
                    Dimensions = new List<string>()
                    {
                        user.Name
                    }
                };
                metrics.Items.Add(metric);
            }

            return metrics;
        }
    }
}
