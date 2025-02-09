using CFIssueTrackerCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFIssueTrackerCommon.EntityWriter
{
    public class CSVProjectWriter : IEntityWriter<Project>
    {
        public void Write(IEnumerable<Project> projects)
        {
            foreach(var project in projects)
            {
                Write(project);
            }
        }

        private void Write(Project project)
        {

        }
    }
}
