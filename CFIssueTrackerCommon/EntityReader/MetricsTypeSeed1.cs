using CFIssueTrackerCommon.Enums;
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
            var list = new List<MetricsType>()
            {
                // Issue metrics
                new MetricsType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Issues By Project",
                    EntityType = EntityTypes.Issue, 
                    DimensionPropertyNames = $"Issue.{nameof(Issue.ProjectId)}",
                    Color = Color.Green.ToArgb().ToString(),
                    ImageSource = "metrics_type.png"
                },
                new MetricsType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Issues By Project Component",
                    EntityType = EntityTypes.Issue,
                    DimensionPropertyNames = $"Issue.{nameof(Issue.ProjectComponentId)}",
                    Color = Color.Green.ToArgb().ToString(),
                    ImageSource = "metrics_type.png"
                },
                new MetricsType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Issues By Status",
                    EntityType = EntityTypes.Issue,
                    DimensionPropertyNames = $"Issue.{nameof(Issue.StatusId)}",
                    Color = Color.Green.ToArgb().ToString(),
                    ImageSource = "metrics_type.png"
                },
                new MetricsType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Issues By Type",
                    EntityType = EntityTypes.Issue,
                    DimensionPropertyNames = $"Issue.{nameof(Issue.TypeId)}",
                    Color = Color.Green.ToArgb().ToString(),
                    ImageSource = "metrics_type.png"
                },
                new MetricsType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Issues By Created User",
                    EntityType = EntityTypes.Issue,
                    DimensionPropertyNames = $"Issue.{nameof(Issue.CreatedUserId)}",
                    Color = Color.Green.ToArgb().ToString(),
                    ImageSource = "metrics_type.png"
                },
                new MetricsType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Issues By Assigned User",
                    EntityType = EntityTypes.Issue,
                    DimensionPropertyNames = $"Issue.{nameof(Issue.AssignedUserId)}",
                    Color = Color.Green.ToArgb().ToString(),
                    ImageSource = "metrics_type.png"
                },
                new MetricsType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Issues By Tag",
                    DimensionPropertyNames = $"Issue.{nameof(TagReference.TagId)}",
                    EntityType = EntityTypes.Issue,
                    Color = Color.Green.ToArgb().ToString(),
                    ImageSource = "metrics_type.png"
                },
                new MetricsType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Issues By Assigned User & Status",
                    EntityType = EntityTypes.Issue,
                    DimensionPropertyNames = $"Issue.{nameof(Issue.AssignedUserId)},Issue.{nameof(Issue.StatusId)}",
                    Color = Color.Green.ToArgb().ToString(),
                    ImageSource = "metrics_type.png"
                },
                new MetricsType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Issues By Assigned User & Type",
                    EntityType = EntityTypes.Issue,
                    DimensionPropertyNames = $"Issue.{nameof(Issue.AssignedUserId)},Issue.{nameof(Issue.TypeId)}",
                    Color = Color.Green.ToArgb().ToString(),
                    ImageSource = "metrics_type.png"
                },

                // Audit event metrics
                new MetricsType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Audit Events By Type",
                    EntityType = EntityTypes.AuditEvent,
                    DimensionPropertyNames = $"AuditEvent.{nameof(Issue.TypeId)}",
                    Color = Color.Green.ToArgb().ToString(),
                    ImageSource = "metrics_type.png"
                },
                new MetricsType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Audit Events By Created User & Type",
                    EntityType = EntityTypes.AuditEvent,
                    DimensionPropertyNames = $"AuditEvent.{nameof(AuditEvent.CreatedUserId)},AuditEvent.{nameof(AuditEvent.TypeId)}",
                    Color = Color.Green.ToArgb().ToString(),
                    ImageSource = "metrics_type.png"
                },
            };

            return list;
        }
    }
}
