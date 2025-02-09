using CFIssueTrackerCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFIssueTrackerCommon.EntityWriter
{
    public class CSVIssueWriter : IEntityWriter<Issue>
    {
        public void Write(IEnumerable<Issue> issues)
        {

            foreach (var issue in issues)
            {
                Write(issue);
            }
        }

        private void Write(Issue issue)
        {

        }
    }
}
