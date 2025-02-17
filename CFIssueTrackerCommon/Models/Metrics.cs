using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFIssueTrackerCommon.Models
{
    public class Metrics
    {
        public string Title { get; set; } = String.Empty;

        public List<Metric> Items = new List<Metric>();
    }
}
