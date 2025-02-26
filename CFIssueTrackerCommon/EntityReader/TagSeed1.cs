using CFIssueTrackerCommon.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFIssueTrackerCommon.EntityReader
{
    public class TagSeed1 : IEntityReader<Tag>
    {
        public IEnumerable<Tag> Read()
        {
            var list = new List<Tag>()
            {
                new Tag()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Tag 1",                    
                },
                new Tag()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Tag 2",
                },
                new Tag()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Tag 3",
                },
                new Tag()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Tag 4",
                },
                new Tag()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Tag 1",
                },
            };

            return list;
        }
    }
}
