using CFIssueTrackerCommon.Models;

namespace CFIssueTrackerCommon.Interfaces
{
    /// <summary>
    /// Returns email content
    /// </summary>
    public interface IEmailCreator
    {
        string Name { get; }

        List<string> GetRecipientEmails(List<SystemValue> systemValues);

        /// <summary>
        /// Returns email subject
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        string GetSubject(List<SystemValue> systemValues);

        /// <summary>
        /// Returns email body
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        string GetBody(List<SystemValue> systemValues);
    }
}
