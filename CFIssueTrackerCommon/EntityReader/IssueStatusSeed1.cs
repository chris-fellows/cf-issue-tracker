using CFIssueTrackerCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace CFIssueTrackerCommon.EntityReader
{
    /// <summary>
    /// Issue status seed #1
    /// </summary>
    public class IssueStatusSeed1 : IEntityReader<IssueStatus>
    {
        public IEnumerable<IssueStatus> Read()
        {
            var list = new List<IssueStatus>()
            {
                new IssueStatus()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "New",
                    Color = Color.Blue.ToArgb().ToString()
                },
                new IssueStatus()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Pending",
                    Color = Color.Red.ToArgb().ToString()
                },
                new IssueStatus()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Active",
                    Color = Color.Green.ToArgb().ToString()
                },
                new IssueStatus()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Resolved [Fixed]",
                    Color = Color.Green.ToArgb().ToString()
                },
                new IssueStatus()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Resolved [No action]",
                    Color = Color.Brown.ToArgb().ToString()
                },
            };

            return list;
        }
    }
}
