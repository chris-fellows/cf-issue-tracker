﻿using CFIssueTrackerCommon.Constants;
using CFIssueTrackerCommon.Interfaces;
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
    /// User seed #1
    /// </summary>
    public class UserSeed1 : IEntityReader<User>
    {
        private readonly IPasswordService _passwordService;

        public UserSeed1(IPasswordService passwordService)
        {
            _passwordService = passwordService;
        }

        public IEnumerable<User> Read()
        {
            var list = new List<User>()
            {
                new User()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "System",
                    Password = "",
                    Role = UserRoleNames.Administrator,                  
                    Active = true,
                    Color = Color.Blue.ToArgb().ToString(),
                    ImageSource = "user.png"
                },
                new User()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Test User 1",
                    Email = "testuser1@domain.com",
                    Password = "testuser1",
                    Role = UserRoleNames.Administrator,
                    Active = true,
                    Color = Color.Orange.ToArgb().ToString(),
                    ImageSource = "user.png"
                },
                new User()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Test User 2",
                    Email = "testuser2@domain.com",
                    Password = "testuser2",
                    Role = UserRoleNames.User,                    
                    Active = true,
                    Color = Color.Brown.ToArgb().ToString(),
                    ImageSource = "user.png"
                },
                new User()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Test User 3",
                    Email = "testuser3@domain.com",
                    Password = "testuser3",
                    Role = UserRoleNames.User,
                    Active = false,
                    Color = Color.Yellow.ToArgb().ToString(),
                    ImageSource = "user.png"
                },
            };

            // Encrypt passwords
            foreach(var user in list.Where(u => !String.IsNullOrEmpty(u.Password)))
            {
                var encrypted = _passwordService.Encrypt(user.Password);
                user.Password = encrypted[0];
                user.Salt = encrypted[1];
            }

            return list;
        }
    }
}
