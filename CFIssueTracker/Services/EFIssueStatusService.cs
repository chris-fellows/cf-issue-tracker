using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;
using Microsoft.EntityFrameworkCore;

namespace CFIssueTrackerCommon.Services
{
    public class EFIssueStatusService : IIssueStatusService
    {
        private readonly IDbContextFactory<CFIssueTracker.Data.CFIssueTrackerContext> _dbFactory;

        public EFIssueStatusService(IDbContextFactory<CFIssueTracker.Data.CFIssueTrackerContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<List<IssueStatus>> GetAllAsync()
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                var issueStatuses = context.IssueStatus;
                return await issueStatuses.ToListAsync();
            }
        }

        public async Task<IssueStatus> AddAsync(IssueStatus issueStatus)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                context.IssueStatus.Add(issueStatus);
                await context.SaveChangesAsync();
                return issueStatus;
            }
        }

        public async Task<IssueStatus> UpdateAsync(IssueStatus issueStatus)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                context.IssueStatus.Update(issueStatus);
                await context.SaveChangesAsync();
                return issueStatus;
            }
        }

        public async Task<IssueStatus?> GetByIdAsync(string id)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                var issueStatus = await context.IssueStatus.FirstOrDefaultAsync(i => i.Id == id);
                return issueStatus;
            }
        }
    }
}
