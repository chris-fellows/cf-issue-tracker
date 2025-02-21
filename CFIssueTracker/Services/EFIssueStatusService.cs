using CFIssueTrackerCommon.Data;
using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;
using Microsoft.EntityFrameworkCore;

namespace CFIssueTrackerCommon.Services
{
    public class EFIssueStatusService : IIssueStatusService
    {
        private readonly IDbContextFactory<CFIssueTrackerContext> _dbFactory;        

        public EFIssueStatusService(IDbContextFactory<CFIssueTrackerContext> dbFactory)
        {
            _dbFactory = dbFactory;            
        }    

        public List<IssueStatus> GetAll()
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                return context.IssueStatus.OrderBy(i => i.Name).ToList();
            }
        }

        public async Task<List<IssueStatus>> GetAllAsync()
        {
            //var context = GetContext();
            //return await context.IssueStatus.ToListAsync();

            using (var context = _dbFactory.CreateDbContext())
            {
                return (await context.IssueStatus.ToListAsync()).OrderBy(i => i.Name).ToList();
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

        public async Task<List<IssueStatus>> GetByIdsAsync(List<string> ids)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                return await context.IssueStatus.Where(i => ids.Contains(i.Id)).ToListAsync();
            }
        }
    }
}
