namespace CFIssueTrackerCommon.Models
{
    /// <summary>
    /// Single metric
    /// </summary>
    public class Metric
    {
        /// <summary>
        /// Metric value
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Dimensions for metric.
        /// 
        /// Dimension can be any of the following:
        /// - Entity name (string). E.g. Issue status
        /// - Time period (DateTimeOffset[]). E.g. Current year
        /// </summary>
        public List<object> Dimensions = new List<object>();        
    }
}
