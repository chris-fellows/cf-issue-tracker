using CFIssueTrackerCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFIssueTrackerCommon.SeedData
{
    public class ProjectSeed1
    {
        public void Load()
        {
            var list = new List<Project>()
            {
                new Project()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Project 1"
                },
                new Project()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Project 2"
                }
            };
        }
    }
}
