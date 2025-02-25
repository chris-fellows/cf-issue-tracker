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
                new MetricsType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Issues By Project",
                    Color = Color.Green.ToArgb().ToString(),
                    ImageSource = "metrics_type.png"
                },
                new MetricsType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Issues By Status",
                    Color = Color.Green.ToArgb().ToString(),
                    ImageSource = "metrics_type.png"
                },
                new MetricsType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Issues By Type",
                    Color = Color.Green.ToArgb().ToString(),
                    ImageSource = "metrics_type.png"
                },
                new MetricsType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Issues By Created User",
                    Color = Color.Green.ToArgb().ToString(),
                    ImageSource = "metrics_type.png"
                },
                new MetricsType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Issues By Assigned User",
                    Color = Color.Green.ToArgb().ToString(),
                    ImageSource = "metrics_type.png"
                }
            };

            return list;
        }
    }
}
