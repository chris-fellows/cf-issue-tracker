using CFIssueTrackerCommon.Data;
using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;
using Microsoft.EntityFrameworkCore;

namespace CFIssueTracker.Services
{
    public class EFMetricsTypeService : IMetricsTypeService
    {
        private readonly IDbContextFactory<CFIssueTrackerContext> _dbFactory;

        public EFMetricsTypeService(IDbContextFactory<CFIssueTrackerContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public List<MetricsType> GetAll()
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                return context.MetricsType.OrderBy(m => m.Name).ToList();
            }
        }

        public async Task<List<MetricsType>> GetAllAsync()
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                return (await context.MetricsType.ToListAsync()).OrderBy(m => m.Name).ToList();
            }
        }

        public async Task<MetricsType> AddAsync(MetricsType metricsType)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                context.MetricsType.Add(metricsType);
                await context.SaveChangesAsync();
                return metricsType;
            }
        }

        public async Task<MetricsType> UpdateAsync(MetricsType metricsType)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                context.MetricsType.Update(metricsType);
                await context.SaveChangesAsync();
                return metricsType;
            }
        }

        public async Task<MetricsType?> GetByIdAsync(string id)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                var metricsType = await context.MetricsType.FirstOrDefaultAsync(i => i.Id == id);
                return metricsType;
            }
        }

        public async Task<List<MetricsType>> GetByIdsAsync(List<string> ids)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                return await context.MetricsType.Where(i => ids.Contains(i.Id)).ToListAsync();
            }
        }
    }
}
