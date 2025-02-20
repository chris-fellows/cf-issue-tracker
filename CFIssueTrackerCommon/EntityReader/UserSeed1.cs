using CFIssueTrackerCommon.Constants;
using CFIssueTrackerCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFIssueTrackerCommon.EntityReader
{
    /// <summary>
    /// User seed #1
    /// </summary>
    public class UserSeed1 : IEntityReader<User>
    {
        public IEnumerable<User> Read()
        {
            var list = new List<User>()
            {
                new User()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "System",
                    Password = "123",
                    Role = UserRoleNames.Administrator,
                    Active = true
                },
                new User()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Test User 1",
                    Email = "testuser1@domain.com",
                    Password = "testuser1",
                    Role = UserRoleNames.Administrator,
                    Active = true
                },
                new User()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Test User 2",
                    Email = "testuser2@domain.com",
                    Password = "testuser2",
                    Role = UserRoleNames.User,                    
                    Active = true
                },
                new User()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Test User 3",
                    Email = "testuser3@domain.com",
                    Password = "testuser3",
                    Role = UserRoleNames.User,
                    Active = false
                },
            };

            return list;
        }
    }
}
