using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFIssueTrackerCommon.Models
{
    /// <summary>
    /// Project component
    /// </summary>
    public class ProjectComponent
    {
        public string Id { get; set; } = String.Empty;

        public string ProjectId { get; set; } = String.Empty;

        public string Name { get; set; } = String.Empty;
    }
}
