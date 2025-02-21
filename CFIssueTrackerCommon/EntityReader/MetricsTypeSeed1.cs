using CFIssueTrackerCommon.Models;

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
                    Name = "Issues By Project"
                },
                new MetricsType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Issues By Status"
                },
                new MetricsType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Issues By Type"
                },
                new MetricsType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Issues By Created User"
                },
                new MetricsType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Issues By Assigned User"
                }
            };

            return list;
        }
    }
}
