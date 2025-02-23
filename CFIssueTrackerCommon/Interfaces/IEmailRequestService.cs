using CFIssueTrackerCommon.Models;

namespace CFIssueTrackerCommon.Interfaces
{
    /// <summary>
    /// Handles requests to send email. Ensures that email is scheduled to be sent but does not send it immediately.
    /// </summary>
    public interface IEmailRequestService
    {
        /// <summary>
        /// Adds request to send Issue Assigned email
        /// </summary>
        /// <param name="issue"></param>
        /// <returns></returns>
        Task AddIssueAssignedAsync(Issue issue);

        /// <summary>
        /// Adds request to send Reset Password email
        /// </summary>
        /// <param name="passwordReset"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        Task AddResetPasswordAsync(PasswordReset passwordReset, User user);

        /// <summary>
        /// Adds request to send New User email
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task AddNewUserAsync(User user);
    }
}
