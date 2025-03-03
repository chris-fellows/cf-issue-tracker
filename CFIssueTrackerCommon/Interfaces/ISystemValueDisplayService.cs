using CFIssueTrackerCommon.Models;

namespace CFIssueTrackerCommon.Interfaces
{
    /// <summary>
    /// Interface for display items for system values
    /// </summary>
    public interface ISystemValueDisplayService
    {
        /// <summary>
        /// Returns display items (List of label and display value)
        /// </summary>
        /// <param name="systemValue"></param>
        /// <returns>List of [Label, DisplayValue]</returns>
        Task<List<string[]>> GetDisplayItemsAsync(SystemValue systemValue);
    }
}
