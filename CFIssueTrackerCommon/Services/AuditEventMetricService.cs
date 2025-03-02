using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;
using CFIssueTrackerCommon.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFIssueTrackerCommon.Services
{
    public class AuditEventMetricService : IAuditEventMetricService
    {
        private readonly IAuditEventService _auditEventService;
        private readonly IAuditEventTypeService _auditEventTypeService;
        private readonly IIssueService _issueService;
        private readonly IIssueStatusService _issueStatusService;
        private readonly IIssueTypeService _issueTypeService;
        private readonly IProjectService _projectService;
        private readonly ITagService _tagService;
        private readonly IUserService _userService;
      
        public AuditEventMetricService(IAuditEventService auditEventService,
                    IAuditEventTypeService auditEventTypeService,
                    IIssueService issueService,
                    IIssueStatusService issueStatusService,
                    IIssueTypeService issueTypeService,
                    IProjectService projectService,
                    ITagService tagService,
                    IUserService userService)
        {
            _auditEventService = auditEventService;
            _auditEventTypeService = auditEventTypeService;
            _issueService = issueService;
            _issueStatusService = issueStatusService;
            _issueTypeService = issueTypeService;
            _projectService = projectService;
            _tagService = tagService;
            _userService = userService;
        }
  
        public async Task<Metrics> GetAuditEventCountAsync(AuditEventFilter filter, List<string> propertyNameDimensions,
                                                        bool excludeZeroValues)
        {
            // Get issues for filter
            var auditEvents = await _auditEventService.GetByFilterAsync(filter);

            // Get dimension colors
            var dimensionColors = MetricUtilities.GetDimensionColors();

            var metrics = new Metrics() { Title = $"Audit Events By {String.Join(" & ", propertyNameDimensions.Select(p => GetPropertyDisplayName(p)))}" };

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
            foreach (var dimensions in dimensionCombinations)
            {
                // Get issue count for dimension
                int auditEventCount = auditEvents.Count(i => dimensions.All(d => IsAuditEventMatches(i, d)));

                if (auditEventCount > 0 || !excludeZeroValues)    // May need to exclude zero values
                {
                    metrics.Items.Add(new Metric()
                    {
                        Value = auditEventCount,
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
        /// Whether audit event matches dimension
        /// </summary>
        /// <param name="issue"></param>
        /// <param name="dimensionInfo"></param>
        /// <returns></returns>
        private bool IsAuditEventMatches(AuditEvent auditEvent, DimensionInfo dimensionInfo)
        {
            switch (dimensionInfo.PropertyName)
            {
                case "AuditEvent.CreatedUserId":
                    return auditEvent.CreatedUserId == dimensionInfo.Id;
                case "AuditEvent.TypeId":
                    return auditEvent.TypeId == dimensionInfo.Id;                
            }

            return false;
        }

        private string GetPropertyDisplayName(string propertyName)
        {
            return propertyName switch
            {
                "AuditEvent.CreatedUserId" => "Created User",
                "AuditEvent.TypeId" => "Type",               
                _ => propertyName
            };
        }

        /// <summary>
        /// Gets all dimension infos for property.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        private async Task<List<DimensionInfo>> GetDimensionInfosForProperty(string propertyName)
        {
            switch (propertyName)
            {
                case "AuditEvent.CreatedUserId":
                    return (await _userService.GetAllAsync()).Select(u =>
                                new DimensionInfo() { Name = u.Name, Id = u.Id, Color = u.Color, PropertyName = propertyName })
                                .OrderBy(u => u.Name).ToList();
                case "AuditEvent.TypeId":
                    return (await _auditEventTypeService.GetAllAsync()).Select(u =>
                                new DimensionInfo() { Name = u.Name, Id = u.Id, Color = u.Color, PropertyName = propertyName })
                                .OrderBy(u => u.Name).ToList();               
                default:
                    return new();
            }
        }
    }
}
