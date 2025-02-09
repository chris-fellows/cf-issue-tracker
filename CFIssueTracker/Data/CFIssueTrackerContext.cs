using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CFIssueTrackerCommon.Models;

namespace CFIssueTracker.Data
{
    public class CFIssueTrackerContext : DbContext
    {
        public CFIssueTrackerContext (DbContextOptions<CFIssueTrackerContext> options)
            : base(options)
        {
        }

        public DbSet<CFIssueTrackerCommon.Models.Issue> Issue { get; set; } = default!;
        public DbSet<CFIssueTrackerCommon.Models.Project> Project { get; set; } = default!;
        public DbSet<CFIssueTrackerCommon.Models.IssueStatus> IssueStatus { get; set; } = default!;
        public DbSet<CFIssueTrackerCommon.Models.IssueType> IssueType { get; set; } = default!;
        public DbSet<CFIssueTrackerCommon.Models.User> User { get; set; } = default!;
    }
}
