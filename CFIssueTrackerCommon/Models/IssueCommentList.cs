using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFIssueTrackerCommon.Models
{
    public class IssueCommentList
    {
        public string Id = Guid.NewGuid().ToString();

        public List<IssueComment> List { get; set; } = new();
    }
}
