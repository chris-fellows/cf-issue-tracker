using CFIssueTrackerCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFIssueTrackerCommon.SeedData
{
    public class IssueTypeSeed1
    {
        public void Load()
        {
            var list = new List<IssueType>()
            {
                new IssueType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Bug"
                },
                new IssueType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "New feature"
                },
                new IssueType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Unknown"
                },
            };
        }
    }
}
