using CFIssueTrackerCommon.Models;

namespace CFIssueTrackerCommon.Interfaces
{
    /// <summary>
    /// Interface for issue metrics    
    /// </summary>
    public interface IIssueMetricService
    {       
        /// <summary>
        /// Gets issue count by one or more property dimensions. E.g. By Status / By Project & Tag        
        /// </summary>
        /// <param name="issueFilter"></param>
        /// <param name="propertyNameDimensions"></param>
        /// <param name="excludeZeroValues">Exclude zero metrics (If false then returns every combination of dimensions)</param>
        /// <returns></returns>
        Task<Metrics> GetIssueCountAsync(IssueFilter filter, List<string> propertyNameDimensions, bool excludeZeroValues);
    }
}
