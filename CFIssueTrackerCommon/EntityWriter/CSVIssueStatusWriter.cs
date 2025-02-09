using CFIssueTrackerCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
