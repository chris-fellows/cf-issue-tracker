using CFIssueTrackerCommon.Data;
using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;
using Microsoft.EntityFrameworkCore;

namespace CFIssueTracker.Services
{
    public class EFSystemTaskTypeService : ISystemTaskTypeService
    {
        private readonly IDbContextFactory<CFIssueTrackerContext> _dbFactory;

        public EFSystemTaskTypeService(IDbContextFactory<CFIssueTrackerContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public List<SystemTaskType> GetAll()
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                return context.SystemTaskType.OrderBy(p => p.Name).ToList();
            }
        }

        public async Task<List<SystemTaskType>> GetAllAsync()
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                return (await context.SystemTaskType.ToListAsync()).OrderBy(p => p.Name).ToList();
            }
        }

        public async Task<SystemTaskType> AddAsync(SystemTaskType systemTaskType)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                context.SystemTaskType.Add(systemTaskType);
                await context.SaveChangesAsync();
                return systemTaskType;
            }
        }

        public async Task<SystemTaskType> UpdateAsync(SystemTaskType systemTaskType)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                context.SystemTaskType.Update(systemTaskType);
                await context.SaveChangesAsync();
                return systemTaskType;
            }
        }

        public async Task<SystemTaskType?> GetByIdAsync(string id)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                var project = await context.SystemTaskType.FirstOrDefaultAsync(i => i.Id == id);
                return project;
            }
        }

        public async Task<List<SystemTaskType>> GetByIdsAsync(List<string> ids)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                return await context.SystemTaskType.Where(i => ids.Contains(i.Id)).ToListAsync();
            }
        }

        public async Task DeleteByIdAsync(string id)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                var systemTaskType = await context.SystemTaskType.FirstOrDefaultAsync(i => i.Id == id);
                if (systemTaskType != null)
                {
                    context.SystemTaskType.Remove(systemTaskType);
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task<SystemTaskType?> GetByNameAsync(string name)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                var systemTaskType = await context.SystemTaskType.FirstOrDefaultAsync(i => i.Name == name);
                return systemTaskType;
            }
        }
    }
}
