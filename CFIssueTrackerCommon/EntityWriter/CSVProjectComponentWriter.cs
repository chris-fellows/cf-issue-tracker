using CFIssueTrackerCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFIssueTrackerCommon.EntityWriter
{
    public class CSVProjectComponentWriter : IEntityWriter<ProjectComponent>
    {
        public void Write(IEnumerable<ProjectComponent> projectComponents)
        {
            foreach(var projectComponent in projectComponents)
            {
                Write(projectComponent);
            }
        }

        private void Write(ProjectComponent projectComponent)
        {

        }
    }
}
