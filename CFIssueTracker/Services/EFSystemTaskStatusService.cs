using CFIssueTrackerCommon.Data;
using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;
using Microsoft.EntityFrameworkCore;

namespace CFIssueTracker.Services
{
    public class EFSystemTaskStatusService : ISystemTaskStatusService
    {
        private readonly IDbContextFactory<CFIssueTrackerContext> _dbFactory;

        public EFSystemTaskStatusService(IDbContextFactory<CFIssueTrackerContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public List<SystemTaskStatus> GetAll()
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                return context.SystemTaskStatus.OrderBy(p => p.Name).ToList();
            }
        }

        public async Task<List<SystemTaskStatus>> GetAllAsync()
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                return (await context.SystemTaskStatus.ToListAsync()).OrderBy(p => p.Name).ToList();
            }
        }

        public async Task<SystemTaskStatus> AddAsync(SystemTaskStatus systemTaskStatus)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                context.SystemTaskStatus.Add(systemTaskStatus);
                await context.SaveChangesAsync();
                return systemTaskStatus;
            }
        }

        public async Task<SystemTaskStatus> UpdateAsync(SystemTaskStatus systemTaskStatus)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                context.SystemTaskStatus.Update(systemTaskStatus);
                await context.SaveChangesAsync();
                return systemTaskStatus;
            }
        }

        public async Task<SystemTaskStatus?> GetByIdAsync(string id)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                var systemTaskStatus = await context.SystemTaskStatus.FirstOrDefaultAsync(i => i.Id == id);
                return systemTaskStatus;
            }
        }

        public async Task<List<SystemTaskStatus>> GetByIdsAsync(List<string> ids)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                return await context.SystemTaskStatus.Where(i => ids.Contains(i.Id)).ToListAsync();
            }
        }

        public async Task DeleteByIdAsync(string id)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                var systemTaskStatus = await context.SystemTaskStatus.FirstOrDefaultAsync(i => i.Id == id);
                if (systemTaskStatus != null)
                {
                    context.SystemTaskStatus.Remove(systemTaskStatus);
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task<SystemTaskStatus?> GetByNameAsync(string name)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                var systemTaskStatus = await context.SystemTaskStatus.FirstOrDefaultAsync(i => i.Name == name);
                return systemTaskStatus;
            }
        }
    }
}
