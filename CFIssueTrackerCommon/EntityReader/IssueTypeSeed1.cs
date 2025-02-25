using CFIssueTrackerCommon.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFIssueTrackerCommon.EntityReader
{
    /// <summary>
    /// Issue type seed #1
    /// </summary>
    public class IssueTypeSeed1 : IEntityReader<IssueType>
    {
        public IEnumerable<IssueType> Read()
        {
            var list = new List<IssueType>()
            {
                new IssueType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Bug",
                    Color = Color.Red.ToArgb().ToString()
                },
                new IssueType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "New feature",
                    Color = Color.OrangeRed.ToArgb().ToString()
                },
                new IssueType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Unknown",
                    Color = Color.BurlyWood.ToArgb().ToString()
                },
            };

            return list;
        }
    }
}
