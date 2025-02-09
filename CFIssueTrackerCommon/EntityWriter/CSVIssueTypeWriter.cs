using CFIssueTrackerCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFIssueTrackerCommon.EntityWriter
{
    public class CSVIssueTypeWriter : IEntityWriter<IssueType>
    {
        public void Write(IEnumerable<IssueType> issueTypes)
        {
            foreach(var issueType in issueTypes)
            {
                Write(issueType);
            }
        }

        private void Write(IssueType issueType)
        {

        }
    }
}
