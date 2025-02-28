using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Drawing;

namespace CFIssueTrackerCommon.Services
{
    public class MetricService : IMetricService
    {
        private readonly IIssueService _issueService;
        private readonly IIssueStatusService _issueStatusService;
        private readonly IIssueTypeService _issueTypeService;
        private readonly IProjectService _projectService;
        private readonly ITagService _tagService;
        private readonly IUserService _userService;

        /// <summary>
        /// Dimension info
        /// </summary>
        private class DimensionInfo
        {
            public string Name { get; set; } = String.Empty;

            public string Id { get; set; } = String.Empty;

            public string Color { get; set; } = String.Empty;

            public string PropertyName { get; set; } = String.Empty;           
        }

        public MetricService(IIssueService issueService,
            IIssueStatusService issueStatusService,
            IIssueTypeService issueTypeService,
            IProjectService projectService,
            ITagService tagService,
            IUserService userService)
        {
            _issueService = issueService;
            _issueStatusService = issueStatusService;
            _issueTypeService = issueTypeService;
            _projectService = projectService;
            _tagService = tagService;
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

        //public async Task<Metrics> GetIssueCountByProjectMetricsAsync(IssueFilter issueFilter)
        //{
        //    var issues = await _issueService.GetByFilterAsync(issueFilter);
        //    var projects = await _projectService.GetAllAsync();

        //    // Get dimension colors
        //    var dimensionColors = GetDimensionColors();

        //    var metrics = new Metrics() { Title = "Issues By Project" };
            
        //    foreach (var project in projects.OrderBy(p => p.Name))
        //    {
        //        var issuesFound = issues.Where(i => i.ProjectId == project.Id);

        //        var metric = new Metric()
        //        {
        //            Value = issuesFound.Count(),
        //            Dimensions = new List<object>()
        //            {
        //                project.Name
        //            }                    
        //        };
        //        metrics.Items.Add(metric);

        //        if (!metrics.DimensionColors.ContainsKey(project.Name))
        //        {
        //            metrics.DimensionColors.Add(project.Name, GetRGBADimensionColor(project.Color, dimensionColors));
        //            //var color = dimensionColors[++colorIndex];
        //            //metrics.DimensionColors.Add(project.Name, $"{color.R},{color.G},{color.B},{color.A}");
        //        }
        //    }            

        //    return metrics;
        //}

        //public async Task<Metrics> GetIssueCountByStatusMetricsAsync(IssueFilter issueFilter)
        //{
        //    var issues = await _issueService.GetByFilterAsync(issueFilter);
        //    var issueStatuses = await _issueStatusService.GetAllAsync();

        //    // Get dimension colors
        //    var dimensionColors = GetDimensionColors();      

        //    var metrics = new Metrics() { Title = "Issues By Status" };

        //    foreach (var issueStatus in issueStatuses.OrderBy(i => i.Name))
        //    {
        //        var issuesFound = issues.Where(i => i.StatusId == issueStatus.Id);

        //        var metric = new Metric()
        //        {
        //            Value = issuesFound.Count(),
        //            Dimensions = new List<object>()
        //            {
        //                issueStatus.Name
        //            }
        //        };
        //        metrics.Items.Add(metric);

        //        if (!metrics.DimensionColors.ContainsKey(issueStatus.Name))
        //        {
        //            metrics.DimensionColors.Add(issueStatus.Name, GetRGBADimensionColor(issueStatus.Color, dimensionColors));
        //            //var color = dimensionColors[++colorIndex];
        //            //metrics.DimensionColors.Add(issueStatus.Name, $"{color.R},{color.G},{color.B},{color.A}");
        //        }
        //    }

        //    return metrics;
        //}

        //public async Task<Metrics> GetIssueCountByTypeMetricsAsync(IssueFilter issueFilter)
        //{
        //    var issues = await _issueService.GetByFilterAsync(issueFilter);
        //    var issueTypes = await _issueTypeService.GetAllAsync();

        //    // Get dimension colors
        //    var dimensionColors = GetDimensionColors();       

        //    var metrics = new Metrics() { Title = "Issues By Type" };

        //    foreach (var issueType in issueTypes.OrderBy(i => i.Name))
        //    {
        //        var issuesFound = issues.Where(i => i.TypeId == issueType.Id);

        //        var metric = new Metric()
        //        {
        //            Value = issuesFound.Count(),
        //            Dimensions = new List<object>()
        //            {
        //                issueType.Name
        //            }
        //        };
        //        metrics.Items.Add(metric);

        //        if (!metrics.DimensionColors.ContainsKey(issueType.Name))
        //        {
        //            metrics.DimensionColors.Add(issueType.Name, GetRGBADimensionColor(issueType.Color, dimensionColors));
        //            //var color = dimensionColors[++colorIndex];
        //            //metrics.DimensionColors.Add(issueType.Name, $"{color.R},{color.G},{color.B},{color.A}");
        //        }
        //    }

        //    return metrics;
        //}

        //public async Task<Metrics> GetIssueCountByCreatedUserMetricsAsync(IssueFilter issueFilter)
        //{
        //    var issues = await _issueService.GetByFilterAsync(issueFilter);
        //    var users = await _userService.GetAllAsync();

        //    // Get dimension colors
        //    var dimensionColors = GetDimensionColors();

        //    var metrics = new Metrics() { Title = "Issues By Created User" };

        //    foreach (var user in users.OrderBy(u => u.Name))
        //    {
        //        var issuesFound = issues.Where(i => i.CreatedUserId == user.Id);

        //        var metric = new Metric()
        //        {
        //            Value = issuesFound.Count(),
        //            Dimensions = new List<object>()
        //            {
        //                user.Name
        //            }
        //        };
        //        metrics.Items.Add(metric);

        //        if (!metrics.DimensionColors.ContainsKey(user.Name))
        //        {
        //            metrics.DimensionColors.Add(user.Name, GetRGBADimensionColor(user.Color, dimensionColors));                    
        //            //var color = dimensionColors[++colorIndex];
        //            //metrics.DimensionColors.Add(user.Name, $"{color.R},{color.G},{color.B},{color.A}");
        //        }
        //    }

        //    return metrics;
        //}

        //public async Task<Metrics> GetIssueCountByAssignedUserMetricsAsync(IssueFilter issueFilter)
        //{
        //    var issues = await _issueService.GetByFilterAsync(issueFilter);
        //    var users = await _userService.GetAllAsync();

        //    // Get dimension colors
        //    var dimensionColors = GetDimensionColors();            

        //    var metrics = new Metrics() { Title = "Issues By Assigned User" };

        //    foreach (var user in users.OrderBy(u => u.Name))
        //    {
        //        var issuesFound = issues.Where(i => i.AssignedUserId == user.Id);

        //        var metric = new Metric()
        //        {
        //            Value = issuesFound.Count(),
        //            Dimensions = new List<object>()
        //            {
        //                user.Name
        //            }
        //        };
        //        metrics.Items.Add(metric);

        //        if (!metrics.DimensionColors.ContainsKey(user.Name))
        //        {
        //            metrics.DimensionColors.Add(user.Name, GetRGBADimensionColor(user.Color, dimensionColors));
        //            //var color = dimensionColors[++colorIndex];
        //            //metrics.DimensionColors.Add(user.Name, $"{color.R},{color.G},{color.B},{color.A}");
        //        }
        //    }

        //    return metrics;
        //}

        //public async Task<Metrics> GetIssueAssignedCountByUserAndStatusMetricsAsync(IssueFilter issueFilter)
        //{
        //    var issues = await _issueService.GetByFilterAsync(issueFilter);
        //    var issueStatuses = await _issueStatusService.GetAllAsync();
        //    var users = await _userService.GetAllAsync();

        //    // Get dimension colors
        //    var dimensionColors = GetDimensionColors();            
            
        //    var metrics = new Metrics() { Title = "Issues By Assigned User & Status" };

        //    foreach (var user in users.OrderBy(u => u.Name))
        //    {                
        //        foreach (var issueStatus in issueStatuses)
        //        {
        //            var issuesFound = issues.Where(i => i.AssignedUserId == user.Id &&
        //                                            i.StatusId == issueStatus.Id);

        //            var metric = new Metric()
        //            {
        //                Value = issuesFound.Count(),
        //                Dimensions = new List<object>()
        //                {
        //                    user.Name,
        //                    issueStatus.Name
        //                }
        //            };
        //            metrics.Items.Add(metric);

        //            if (!metrics.DimensionColors.ContainsKey(issueStatus.Name))
        //            {
        //                metrics.DimensionColors.Add(issueStatus.Name, GetRGBADimensionColor(issueStatus.Color, dimensionColors));
        //            }
        //            if (!metrics.DimensionColors.ContainsKey(user.Name))
        //            {
        //                metrics.DimensionColors.Add(user.Name, GetRGBADimensionColor(user.Color, dimensionColors));                        
        //            }
        //        }
        //    }

        //    return metrics;
        //}        

        public async Task<Metrics> GetIssueCountAsync(IssueFilter issueFilter, List<string> propertyNameDimensions,
                                                    bool excludeZeroValues)
        {
            // Get issues for filter
            var issues = await _issueService.GetByFilterAsync(issueFilter);
            
            // Get dimension colors
            var dimensionColors = GetDimensionColors();

            var metrics = new Metrics() { Title = $"Issues By {String.Join(" & ", propertyNameDimensions.Select(p => GetPropertyDisplayName(p)))}" };

            // Get all dimensions for each property
            var dimensionsByProperty = new List<List<DimensionInfo>>();
            for (int dimensionIndex = 0; dimensionIndex < propertyNameDimensions.Count; dimensionIndex++)
            {
                var dimensionsForProperty = await GetDimensionInfosForProperty(propertyNameDimensions[dimensionIndex]);
                dimensionsByProperty.Add(dimensionsForProperty);
            }

            // Get combinations of every dimension
            var dimensionCombinations = GetDimensionCombinations(dimensionsByProperty);

            //int issueCount = 0;
            //var mutex = new Mutex();
            //Parallel.ForEach(dimensionNameCombinations,
            //            new ParallelOptions { MaxDegreeOfParallelism = 5 },
            //            (combination) =>
            //            {
            //                int issueCountForCombination = issues.Count(i => combination.All(d => IsIssueMatches(i, d)));
            //            });
            
            // Process each combination of dimensions. May need to exclude zero values.
            foreach(var dimensions in dimensionCombinations)
            {
                // Get issue count for dimension
                int issueCount = issues.Count(i => dimensions.All(d => IsIssueMatches(i, d)));

                if (issueCount > 0 || !excludeZeroValues)    // May need to exclude zero values
                {
                    metrics.Items.Add(new Metric()
                    {
                        Value = issueCount,
                        Dimensions = dimensions.Select(c => (object)c.Name).ToList(),
                    });

                    // Add dimension colors
                    foreach (var dimension in dimensions)
                    {
                        if (!metrics.DimensionColors.ContainsKey(dimension.Name))
                        {
                            metrics.DimensionColors.Add(dimension.Name, GetRGBADimensionColor(dimension.Color, dimensionColors));
                        }
                    }
                }
            }

            return metrics;
        }        

        /// <summary>
        /// Whether issue matches dimension
        /// </summary>
        /// <param name="issue"></param>
        /// <param name="dimensionInfo"></param>
        /// <returns></returns>
        private bool IsIssueMatches(Issue issue, DimensionInfo dimensionInfo)
        {
            switch(dimensionInfo.PropertyName)
            {
                case "AssignedUserId":
                    return issue.AssignedUserId == dimensionInfo.Id;                    
                case "CreatedUserId":
                    return issue.CreatedUserId == dimensionInfo.Id;
                case "StatusId":
                    return issue.StatusId == dimensionInfo.Id;                    
                case "TypeId":
                    return issue.TypeId == dimensionInfo.Id;                    
                case "ProjectId":
                    return issue.ProjectId == dimensionInfo.Id;
                case "TagId":
                    return issue.Tags != null && issue.Tags.Any(t => t.TagId == dimensionInfo.Id);
            }

            return false;
        }

        private string GetPropertyDisplayName(string propertyName)
        {
            return propertyName switch
            {
                "AssignedUserId" => "Assigned User",
                "CreatedUserId" => "Created User",
                "ProjectId" => "Project",
                "StatusId" => "Status",
                "TagId" => "Tag",
                "TypeId" => "Type",
                _ => propertyName
            };
        }

        /// <summary>
        /// Gets all combinations of dimensions
        /// </summary>
        /// <param name="dimensionInfosByProperty"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        private List<List<DimensionInfo>> GetDimensionCombinations(List<List<DimensionInfo>> dimensionInfosByProperty)
        {
            if (dimensionInfosByProperty.Count > 3)
            {
                throw new NotSupportedException("Only combinations of 3 dimensions are supported");
            }

            var combinations = new List<List<DimensionInfo>>();

            // TODO: Handle any number of dimensions
            for (int index1 = 0; index1 < dimensionInfosByProperty[0].Count; index1++)
            {
                if (dimensionInfosByProperty.Count == 1)   // Only 1 dimension
                {
                    combinations.Add(new List<DimensionInfo>() { dimensionInfosByProperty[0][index1] });
                }
                else   // At least 2 dimensions
                {
                    for (int index2 = 0; index2 < dimensionInfosByProperty[1].Count; index2++)
                    {
                        if (dimensionInfosByProperty.Count == 2)   // Only 2 dimensions
                        {
                            combinations.Add(new List<DimensionInfo>() { dimensionInfosByProperty[0][index1], 
                                                                dimensionInfosByProperty[1][index2] });
                        }
                        else   // At least 3 dimensions
                        {
                            for (int index3 = 0; index3 < dimensionInfosByProperty[2].Count; index3++)
                            {                                
                                combinations.Add(new List<DimensionInfo>() { dimensionInfosByProperty[0][index1], 
                                                                dimensionInfosByProperty[1][index2], 
                                                                dimensionInfosByProperty[2][index3] });
                            }
                        }
                    }
                }
            }                

            return combinations;
        }

        /// <summary>
        /// Gets all dimension infos for property. E.g. For issue status then returns issue status names
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        private async Task<List<DimensionInfo>> GetDimensionInfosForProperty(string propertyName)
        {
            switch(propertyName)
            {
                case "AssignedUserId":
                    return (await _userService.GetAllAsync()).Select(u => 
                                new DimensionInfo() { Name = u.Name, Id = u.Id, Color = u.Color, PropertyName = propertyName })
                                .OrderBy(u => u.Name).ToList();
                case "CreatedUserId":
                    return (await _userService.GetAllAsync()).Select(u => 
                                new DimensionInfo() { Name = u.Name, Id = u.Id, Color = u.Color, PropertyName = propertyName })
                                .OrderBy(u => u.Name).ToList();                                
                case "StatusId":
                    return (await _issueStatusService.GetAllAsync()).Select(i => 
                                new DimensionInfo() { Name = i.Name, Id = i.Id, Color = i.Color, PropertyName = propertyName })
                                .OrderBy(u => u.Name).ToList();
                case "TypeId":
                    return (await _issueTypeService.GetAllAsync()).Select(i =>
                                new DimensionInfo() { Name = i.Name, Id = i.Id, Color = i.Color, PropertyName = propertyName })
                                .OrderBy(u => u.Name).ToList();
                case "ProjectId":
                    return (await _projectService.GetAllAsync()).Select(i => 
                                new DimensionInfo() { Name = i.Name, Id = i.Id, Color = i.Color, PropertyName = propertyName })
                                .OrderBy(u => u.Name).ToList();
                case "TagId":
                    return (await _tagService.GetAllAsync()).Select(i => 
                                new DimensionInfo() { Name = i.Name, Id = i.Id, Color = System.Drawing.Color.Blue.ToArgb().ToString(), PropertyName = propertyName })
                                .OrderBy(u => u.Name).ToList();
                default:
                    return new();
            }
        }
    }
}
