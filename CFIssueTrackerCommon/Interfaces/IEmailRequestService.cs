using CFIssueTrackerCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFIssueTrackerCommon.Interfaces
{
    /// <summary>
    /// Handles requests to send email
    /// </summary>
    public interface IEmailRequestService
    {
        /// <summary>
        /// Adds request to send Issue Assigned email
        /// </summary>
        /// <param name="issue"></param>
        /// <returns></returns>
        Task AddIssueAssigned(Issue issue);

        /// <summary>
        /// Adds request to send Reset Password email
        /// </summary>
        /// <param name="passwordReset"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        Task AddResetPassword(PasswordReset passwordReset, User user);

        /// <summary>
        /// Adds request to send New User email
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task AddNewUser(User user);
    }
}
