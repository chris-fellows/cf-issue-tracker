using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;
using System.Drawing;

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

        /// <summary>
        /// Gets dimension color (RGBA format) from input color (Color value, color name, RGBA values)
        /// </summary>
        /// <param name="color"></param>
        /// <param name="colors"></param>
        /// <returns></returns>
        private string GetRGBADimensionColor(string color, List<System.Drawing.Color> colors)
        {
            // Check if list of RGBA values
            if (color.Contains(",")) return color;  // RGBA format

            // Check if color value (RGBA value)
            if (Int32.TryParse(color, out var colorValue))
            {
                var colorItem = Color.FromArgb(colorValue);
                return $"{colorItem.R},{colorItem.G},{colorItem.B},{colorItem.A}";
            }
            
            // Check if color name
            var colorByName = colors.FirstOrDefault(c => c.Name == color);
            if (colorByName != null)
            {
                return $"{colorByName.R},{colorByName.G},{colorByName.B},{colorByName.A}";
            }
            return "";
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
                    metrics.DimensionColors.Add(project.Name, GetRGBADimensionColor(project.Color, dimensionColors));
                    //var color = dimensionColors[++colorIndex];
                    //metrics.DimensionColors.Add(project.Name, $"{color.R},{color.G},{color.B},{color.A}");
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
                    metrics.DimensionColors.Add(issueStatus.Name, GetRGBADimensionColor(issueStatus.Color, dimensionColors));
                    //var color = dimensionColors[++colorIndex];
                    //metrics.DimensionColors.Add(issueStatus.Name, $"{color.R},{color.G},{color.B},{color.A}");
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
                    metrics.DimensionColors.Add(issueType.Name, GetRGBADimensionColor(issueType.Color, dimensionColors));
                    //var color = dimensionColors[++colorIndex];
                    //metrics.DimensionColors.Add(issueType.Name, $"{color.R},{color.G},{color.B},{color.A}");
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
                    metrics.DimensionColors.Add(user.Name, GetRGBADimensionColor(user.Color, dimensionColors));                    
                    //var color = dimensionColors[++colorIndex];
                    //metrics.DimensionColors.Add(user.Name, $"{color.R},{color.G},{color.B},{color.A}");
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
                    metrics.DimensionColors.Add(user.Name, GetRGBADimensionColor(user.Color, dimensionColors));
                    //var color = dimensionColors[++colorIndex];
                    //metrics.DimensionColors.Add(user.Name, $"{color.R},{color.G},{color.B},{color.A}");
                }
            }

            return metrics;
        }
    }
}
