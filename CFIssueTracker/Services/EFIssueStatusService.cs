using CFIssueTracker.Data;
using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;
using Microsoft.EntityFrameworkCore;

namespace CFIssueTrackerCommon.Services
{
    public class EFIssueStatusService : IIssueStatusService //, IDisposable
    {
        private readonly IDbContextFactory<CFIssueTrackerContext> _dbFactory;
        //private CFIssueTrackerContext? _context;

        public EFIssueStatusService(IDbContextFactory<CFIssueTracker.Data.CFIssueTrackerContext> dbFactory)
        {
            _dbFactory = dbFactory;            
        }

        //public void Dispose()
        //{
        //    if (_context != null)
        //    {
        //        _context.Dispose();
        //        _context = null;
        //    }
        //}

        //private CFIssueTrackerContext GetContext()
        //{
        //    if (_context == null)
        //    {
        //        _context = _dbFactory.CreateDbContext();
        //    }
        //    return _context;
        //}

        public async Task<List<IssueStatus>> GetAllAsync()
        {
            //var context = GetContext();
            //return await context.IssueStatus.ToListAsync();

            using (var context = _dbFactory.CreateDbContext())
            {                
                return await context.IssueStatus.ToListAsync();                
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
