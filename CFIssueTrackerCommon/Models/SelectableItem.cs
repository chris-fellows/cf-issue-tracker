using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFIssueTrackerCommon.Models
{
    public class SelectableItem
    {
        public string Id { get; set; } = String.Empty;

        public string Name { get; set; } = String.Empty;

        public bool Selected { get; set; }
    }
}
