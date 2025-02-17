using CFIssueTrackerCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFIssueTrackerCommon.EntityReader
{
    /// <summary>
    /// Project component seed #1
    /// </summary>
    public class ProjectComponentSeed1 : IEntityReader<ProjectComponent>
    {
        public IEnumerable<ProjectComponent> Read()
        {
            var list = new List<ProjectComponent>()
            {
                new ProjectComponent()
                {
                    Id = Guid.NewGuid().ToString(),                    
                    Name = "Unknown"
                }
            };

            return list;
        }
    }
}
