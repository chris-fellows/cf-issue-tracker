using CFIssueTrackerCommon.Models;

namespace CFIssueTrackerCommon.EntityWriter
{
    public class CSVIssueStatusWriter : IEntityWriter<IssueStatus>
    {
        public void Write(IEnumerable<IssueStatus> issueStatuses)
        {
            foreach (var issueStatus in issueStatuses)
            {
                Write(issueStatus);
            }
        }

        private void Write(IssueStatus issueStatus)
        {

        }
    }
}
