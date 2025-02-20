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

        /// <summary>
        /// Returns colors for dimensions
        /// 
        /// TODO: Store color in DB so that we can configure color for specific entities
        /// </summary>
        /// <returns></returns>
        private static List<System.Drawing.Color> GetDimensionColors()
        {
            var colors = new List<System.Drawing.Color>();
            var properties = typeof(System.Drawing.Color).GetProperties().Where(p =>
                            p.PropertyType == typeof(System.Drawing.Color) &&
                            p.ReflectedType == typeof(System.Drawing.Color)).ToList();
            foreach (var property in properties)
            {
                var color = (System.Drawing.Color)property.GetValue(null);
                if (color != System.Drawing.Color.Transparent) colors.Add(color);
            }
            return colors;
        }

        public async Task<Metrics> GetIssueCountByProjectMetricsAsync(IssueFilter issueFilter)
        {
            var issues = await _issueService.GetByFilterAsync(issueFilter);
            var projects = await _projectService.GetAllAsync();

            // Get dimension colors
            var dimensionColors = GetDimensionColors();
            int colorIndex = -1;

            var metrics = new Metrics() { Title = "Issues By Project" };
            
            foreach (var project in projects.OrderBy(p => p.Name))
            {
                var issuesFound = issues.Where(i => i.ProjectId == project.Id);

                var metric = new Metric()
                {
                    Value = issuesFound.Count(),
                    Dimensions = new List<object>()
                    {
                        project.Name
                    }                    
                };
                metrics.Items.Add(metric);

                if (!metrics.DimensionColors.ContainsKey(project.Name))
                {
                    var color = dimensionColors[++colorIndex];
                    metrics.DimensionColors.Add(project.Name, $"{color.R},{color.G},{color.B},{color.A}");
                }
            }            

            return metrics;
        }

        public async Task<Metrics> GetIssueCountByStatusMetricsAsync(IssueFilter issueFilter)
        {
            var issues = await _issueService.GetByFilterAsync(issueFilter);
            var issueStatuses = await _issueStatusService.GetAllAsync();

            // Get dimension colors
            var dimensionColors = GetDimensionColors();
            int colorIndex = -1;

            var metrics = new Metrics() { Title = "Issues By Status" };

            foreach (var issueStatus in issueStatuses.OrderBy(i => i.Name))
            {
                var issuesFound = issues.Where(i => i.StatusId == issueStatus.Id);

                var metric = new Metric()
                {
                    Value = issuesFound.Count(),
                    Dimensions = new List<object>()
                    {
                        issueStatus.Name
                    }
                };
                metrics.Items.Add(metric);

                if (!metrics.DimensionColors.ContainsKey(issueStatus.Name))
                {
                    var color = dimensionColors[++colorIndex];
                    metrics.DimensionColors.Add(issueStatus.Name, $"{color.R},{color.G},{color.B},{color.A}");
                }
            }

            return metrics;
        }

        public async Task<Metrics> GetIssueCountByTypeMetricsAsync(IssueFilter issueFilter)
        {
            var issues = await _issueService.GetByFilterAsync(issueFilter);
            var issueTypes = await _issueTypeService.GetAllAsync();

            // Get dimension colors
            var dimensionColors = GetDimensionColors();
            int colorIndex = -1;

            var metrics = new Metrics() { Title = "Issues By Type" };

            foreach (var issueType in issueTypes.OrderBy(i => i.Name))
            {
                var issuesFound = issues.Where(i => i.TypeId == issueType.Id);

                var metric = new Metric()
                {
                    Value = issuesFound.Count(),
                    Dimensions = new List<object>()
                    {
                        issueType.Name
                    }
                };
                metrics.Items.Add(metric);

                if (!metrics.DimensionColors.ContainsKey(issueType.Name))
                {
                    var color = dimensionColors[++colorIndex];
                    metrics.DimensionColors.Add(issueType.Name, $"{color.R},{color.G},{color.B},{color.A}");
                }
            }

            return metrics;
        }

        public async Task<Metrics> GetIssueCountByCreatedUserMetricsAsync(IssueFilter issueFilter)
        {
            var issues = await _issueService.GetByFilterAsync(issueFilter);
            var users = await _userService.GetAllAsync();

            // Get dimension colors
            var dimensionColors = GetDimensionColors();
            int colorIndex = -1;

            var metrics = new Metrics() { Title = "Issues By Created User" };

            foreach (var user in users.OrderBy(u => u.Name))
            {
                var issuesFound = issues.Where(i => i.CreatedUserId == user.Id);

                var metric = new Metric()
                {
                    Value = issuesFound.Count(),
                    Dimensions = new List<object>()
                    {
                        user.Name
                    }
                };
                metrics.Items.Add(metric);

                if (!metrics.DimensionColors.ContainsKey(user.Name))
                {
                    var color = dimensionColors[++colorIndex];
                    metrics.DimensionColors.Add(user.Name, $"{color.R},{color.G},{color.B},{color.A}");
                }
            }

            return metrics;
        }

        public async Task<Metrics> GetIssueCountByAssignedUserMetricsAsync(IssueFilter issueFilter)
        {
            var issues = await _issueService.GetByFilterAsync(issueFilter);
            var users = await _userService.GetAllAsync();

            // Get dimension colors
            var dimensionColors = GetDimensionColors();
            int colorIndex = -1;

            var metrics = new Metrics() { Title = "Issues By Assigned User" };

            foreach (var user in users.OrderBy(u => u.Name))
            {
                var issuesFound = issues.Where(i => i.AssignedUserId == user.Id);

                var metric = new Metric()
                {
                    Value = issuesFound.Count(),
                    Dimensions = new List<object>()
                    {
                        user.Name
                    }
                };
                metrics.Items.Add(metric);

                if (!metrics.DimensionColors.ContainsKey(user.Name))
                {
                    var color = dimensionColors[++colorIndex];
                    metrics.DimensionColors.Add(user.Name, $"{color.R},{color.G},{color.B},{color.A}");
                }
            }

            return metrics;
        }
    }
}
