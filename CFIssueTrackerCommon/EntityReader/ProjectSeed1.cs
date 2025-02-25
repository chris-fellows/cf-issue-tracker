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
    /// Project seed #1
    /// </summary>
    public class ProjectSeed1 : IEntityReader<Project>
    {
        public IEnumerable<Project> Read()
        {
            var list = new List<Project>()
            {
                new Project()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Project 1",
                    Color= Color.Blue.ToArgb().ToString(),
                    ImageSource = "project.png"
                },
                new Project()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Project 2",
                    Color = Color.Orange.ToArgb().ToString(),
                    ImageSource = "project.png"
                }
            };

            return list;
        }
    }
}
