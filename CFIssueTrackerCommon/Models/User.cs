﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFIssueTrackerCommon.Models
{
    /// <summary>
    /// User
    /// </summary>
    public class User
    {
        public string Id { get; set; } = String.Empty;

        public string Name { get; set; } = String.Empty;

        public string Email { get; set; } = String.Empty;

        public bool Active { get; set; } = true;
    }
}
