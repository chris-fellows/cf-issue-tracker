using CFIssueTrackerCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFIssueTrackerCommon.SeedData
{
    public class UserSeed1
    {
        public void Load()
        {
            var list = new List<User>()
            {
                new User()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "System",
                    Active = true
                },
                new User()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Test User 1",
                    Email = "testuser1@domain.com",
                    Active = true
                },
                new User()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Test User 2",
                    Email = "testuser2@domain.com",
                    Active = true
                },
                new User()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Test User 3",
                    Email = "testuser3@domain.com",
                    Active = true
                },
            };
        }
    }
}
