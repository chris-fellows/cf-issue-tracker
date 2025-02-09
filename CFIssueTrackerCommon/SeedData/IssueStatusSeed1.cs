using CFIssueTrackerCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFIssueTrackerCommon.SeedData
{
    public class IssueStatusSeed1
    {
        public void Load()
        {
            var list = new List<IssueStatus>()
            {               
                new IssueStatus()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "New"
                },
                new IssueStatus()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Pending"
                },
                new IssueStatus()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Active"
                },
                new IssueStatus()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Resolved [Fixed]"
                },
                new IssueStatus()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Resolved [No action]"
                },
            };
        }
    }
}
