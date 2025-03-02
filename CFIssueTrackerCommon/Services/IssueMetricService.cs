using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;
using CFIssueTrackerCommon.Utilities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Drawing;

namespace CFIssueTrackerCommon.Services
{
    public class IssueMetricService : IIssueMetricService
    {
        private readonly IIssueService _issueService;
        private readonly IIssueStatusService _issueStatusService;
        private readonly IIssueTypeService _issueTypeService;
        private readonly IProjectComponentService _projectComponentService;
        private readonly IProjectService _projectService;
        private readonly ITagService _tagService;
        private readonly IUserService _userService;

        public IssueMetricService(IIssueService issueService,
            IIssueStatusService issueStatusService,
            IIssueTypeService issueTypeService,
            IProjectComponentService projectComponentService,
            IProjectService projectService,
            ITagService tagService,
            IUserService userService)
        {
            _issueService = issueService;
            _issueStatusService = issueStatusService;
            _issueTypeService = issueTypeService;
            _projectComponentService = projectComponentService;
            _projectService = projectService;
            _tagService = tagService;
            _userService = userService;
        }      
        
        public async Task<Metrics> GetIssueCountAsync(IssueFilter filter, List<string> propertyNameDimensions,
                                                    bool excludeZeroValues)
        {
            // Get issues for filter
            var issues = await _issueService.GetByFilterAsync(filter);
            
            // Get dimension colors
            var dimensionColors = MetricUtilities.GetDimensionColors();

            var metrics = new Metrics() { Title = $"Issues By {String.Join(" & ", propertyNameDimensions.Select(p => GetPropertyDisplayName(p)))}" };

            // Get all dimensions for each property
            var dimensionsByProperty = new List<List<DimensionInfo>>();
            for (int dimensionIndex = 0; dimensionIndex < propertyNameDimensions.Count; dimensionIndex++)
            {
                var dimensionsForProperty = await GetDimensionInfosForProperty(propertyNameDimensions[dimensionIndex]);
                dimensionsByProperty.Add(dimensionsForProperty);
            }

            // Get combinations of every dimension
            var dimensionCombinations = MetricUtilities.GetDimensionCombinations(dimensionsByProperty);

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
                            metrics.DimensionColors.Add(dimension.Name, MetricUtilities.GetRGBADimensionColor(dimension.Color, dimensionColors));
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
                case "Issue.AssignedUserId":
                    return issue.AssignedUserId == dimensionInfo.Id;                    
                case "Issue.CreatedUserId":
                    return issue.CreatedUserId == dimensionInfo.Id;
                case "Issue.StatusId":
                    return issue.StatusId == dimensionInfo.Id;                    
                case "Issue.TypeId":
                    return issue.TypeId == dimensionInfo.Id;
                case "Issue.ProjectComponentId":
                    return issue.ProjectComponentId == dimensionInfo.Id;
                case "Issue.ProjectId":
                    return issue.ProjectId == dimensionInfo.Id;
                case "Issue.TagId":
                    return issue.Tags != null && issue.Tags.Any(t => t.TagId == dimensionInfo.Id);
            }

            return false;
        }

        private string GetPropertyDisplayName(string propertyName)
        {
            return propertyName switch
            {
                "Issue.AssignedUserId" => "Assigned User",
                "Issue.CreatedUserId" => "Created User",
                "Issue.ProjectComponentId" => "Component",
                "Issue.ProjectId" => "Project",
                "Issue.StatusId" => "Status",
                "Issue.TagId" => "Tag",
                "Issue.TypeId" => "Type",
                _ => propertyName
            };
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
                case "Issue.AssignedUserId":
                    return (await _userService.GetAllAsync()).Select(u => 
                                new DimensionInfo() { Name = u.Name, Id = u.Id, Color = u.Color, PropertyName = propertyName })
                                .OrderBy(u => u.Name).ToList();
                case "Issue.CreatedUserId":
                    return (await _userService.GetAllAsync()).Select(u => 
                                new DimensionInfo() { Name = u.Name, Id = u.Id, Color = u.Color, PropertyName = propertyName })
                                .OrderBy(u => u.Name).ToList();                                
                case "Issue.StatusId":
                    return (await _issueStatusService.GetAllAsync()).Select(i => 
                                new DimensionInfo() { Name = i.Name, Id = i.Id, Color = i.Color, PropertyName = propertyName })
                                .OrderBy(u => u.Name).ToList();
                case "Issue.TypeId":
                    return (await _issueTypeService.GetAllAsync()).Select(i =>
                                new DimensionInfo() { Name = i.Name, Id = i.Id, Color = i.Color, PropertyName = propertyName })
                                .OrderBy(u => u.Name).ToList();
                case "Issue.ProjectComponentId":
                    // TODO: Should we add project name because project component relates to project?
                    return (await _projectComponentService.GetAllAsync()).Select(i =>
                                new DimensionInfo() { Name = i.Name, Id = i.Id, Color = i.Color, PropertyName = propertyName })
                                .OrderBy(u => u.Name).ToList();
                case "Issue.ProjectId":
                    return (await _projectService.GetAllAsync()).Select(i => 
                                new DimensionInfo() { Name = i.Name, Id = i.Id, Color = i.Color, PropertyName = propertyName })
                                .OrderBy(u => u.Name).ToList();
                case "Issue.TagId":
                    return (await _tagService.GetAllAsync()).Select(i => 
                                new DimensionInfo() { Name = i.Name, Id = i.Id, Color = System.Drawing.Color.Blue.ToArgb().ToString(), PropertyName = propertyName })
                                .OrderBy(u => u.Name).ToList();
                default:
                    return new();
            }
        }
    }
}
