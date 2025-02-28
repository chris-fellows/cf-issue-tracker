using CFIssueTrackerCommon.Models;
using System.Drawing;

namespace CFIssueTrackerCommon.EntityReader
{
    /// <summary>
    /// Metrics type seed #1
    /// </summary>
    public class MetricsTypeSeed1 : IEntityReader<MetricsType>
    {
        public IEnumerable<MetricsType> Read()
        {
            // Note that for stacked bar charts (Multi-dimension) then 
            var list = new List<MetricsType>()
            {
                new MetricsType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Issues By Project",
                    //DimensionPropertyNames = nameof(Issue.ProjectId),
                    Color = Color.Green.ToArgb().ToString(),
                    ImageSource = "metrics_type.png"
                },
                new MetricsType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Issues By Status",
                   // DimensionPropertyNames = nameof(Issue.StatusId),
                    Color = Color.Green.ToArgb().ToString(),
                    ImageSource = "metrics_type.png"
                },
                new MetricsType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Issues By Type",
                    //DimensionPropertyNames = nameof(Issue.TypeId),
                    Color = Color.Green.ToArgb().ToString(),
                    ImageSource = "metrics_type.png"
                },
                new MetricsType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Issues By Created User",
                    //DimensionPropertyNames = nameof(Issue.CreatedUserId),
                    Color = Color.Green.ToArgb().ToString(),
                    ImageSource = "metrics_type.png"
                },
                new MetricsType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Issues By Assigned User",
                  //  DimensionPropertyNames = nameof(Issue.AssignedUserId),
                    Color = Color.Green.ToArgb().ToString(),
                    ImageSource = "metrics_type.png"
                },
                new MetricsType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Issues By Tag",
                   // DimensionPropertyNames = nameof(TagReference.TagId),
                    Color = Color.Green.ToArgb().ToString(),
                    ImageSource = "metrics_type.png"
                },
                new MetricsType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Issues By Assigned User & Status",
                  //  DimensionPropertyNames = $"{nameof(Issue.AssignedUserId)},{nameof(Issue.StatusId)}",
                    Color = Color.Green.ToArgb().ToString(),
                    ImageSource = "metrics_type.png"
                },
                new MetricsType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Issues By Assigned User & Type",
                   // DimensionPropertyNames = $"{nameof(Issue.AssignedUserId)},{nameof(Issue.TypeId)}",
                    Color = Color.Green.ToArgb().ToString(),
                    ImageSource = "metrics_type.png"
                },
            };

            return list;
        }
    }
}
