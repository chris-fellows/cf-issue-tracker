namespace CFIssueTrackerCommon.Models
{
    /// <summary>
    /// Metrics
    /// </summary>
    public class Metrics
    {
        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; } = String.Empty;

        /// <summary>
        /// Metric items
        /// </summary>
        public List<Metric> Items = new List<Metric>();

        /// <summary>
        /// Dimensions colors keyed by dimension name. RGBA values
        /// </summary>
        public Dictionary<string, string> DimensionColors = new Dictionary<string, string>();
    }
}
