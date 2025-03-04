using CFIssueTrackerCommon.Data;
using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;
using CFIssueTrackerCommon.Services;
using Microsoft.EntityFrameworkCore;

namespace CFIssueTracker.Services
{
    public class EFMetricsTypeService : EFBaseService, IMetricsTypeService
    {        
        public EFMetricsTypeService(IDbContextFactory<CFIssueTrackerContext> dbFactory) : base(dbFactory)
        {
     
        }

        public List<MetricsType> GetAll()
        {            
                return Context.MetricsType.OrderBy(m => m.Name).ToList();         
        }

        public async Task<List<MetricsType>> GetAllAsync()
        {            
                return (await Context.MetricsType.ToListAsync()).OrderBy(m => m.Name).ToList();         
        }

        public async Task<MetricsType> AddAsync(MetricsType metricsType)
        {            
                Context.MetricsType.Add(metricsType);
                await Context.SaveChangesAsync();
                return metricsType;         
        }

        public async Task<MetricsType> UpdateAsync(MetricsType metricsType)
        {            
                Context.MetricsType.Update(metricsType);
                await Context.SaveChangesAsync();
                return metricsType;         
        }

        public async Task<MetricsType?> GetByIdAsync(string id)
        {            
                var metricsType = await Context.MetricsType.FirstOrDefaultAsync(i => i.Id == id);
                return metricsType;         
        }

        public async Task<List<MetricsType>> GetByIdsAsync(List<string> ids)
        {            
                return await Context.MetricsType.Where(i => ids.Contains(i.Id)).ToListAsync();        
        }

        public async Task DeleteByIdAsync(string id)
        {            
                var metricsType = await Context.MetricsType.FirstOrDefaultAsync(i => i.Id == id);
                if (metricsType != null)
                {
                    Context.MetricsType.Remove(metricsType);
                    await Context.SaveChangesAsync();
                }         
        }

        public async Task<MetricsType?> GetByNameAsync(string name)
        {            
                var metricsType = await Context.MetricsType.FirstOrDefaultAsync(i => i.Name == name);
                return metricsType;         
        }
    }
}
