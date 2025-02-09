using CFIssueTracker.Data;
using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFIssueTrackerCommon.Services
{
    public class EFIssueService : IIssueService
    {
        private readonly IDbContextFactory<CFIssueTracker.Data.CFIssueTrackerContext> _dbFactory;

        public EFIssueService(IDbContextFactory<CFIssueTracker.Data.CFIssueTrackerContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<List<Issue>> GetAllAsync()
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                var issues = context.Issue;
                return await issues.ToListAsync();
            }
        }

        public async Task<Issue> AddAsync(Issue issue)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                context.Issue.Add(issue);
                await context.SaveChangesAsync();
                return issue;
            }
        }

        public async Task<Issue?> GetByIdAsync(string id)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                var issue = await context.Issue.FirstOrDefaultAsync(i => i.Id == id);
                return issue;
            }
        }
    }
}
