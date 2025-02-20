using CFIssueTrackerCommon.Models;

namespace CFIssueTrackerCommon.Interfaces
{
    /// <summary>
    /// Request context for current request. Enables us to pass the HTTP request context in to a
    /// service.    
    /// </summary>
    public interface IRequestContextService
    {
        /// <summary>
        /// User Id
        /// </summary>
        public string? UserId { get; }

        /// <summary>
        /// User
        /// </summary>
        public User? User { get; }
    }
}
