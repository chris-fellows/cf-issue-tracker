using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFIssueTrackerCommon.Models
{
    public class Metric
    {
        public double Value { get; set; }

        public List<string> Dimensions = new List<string>();
    }
}
