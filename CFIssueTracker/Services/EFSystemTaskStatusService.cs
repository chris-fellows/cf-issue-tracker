using CFIssueTrackerCommon.Data;
using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;
using CFIssueTrackerCommon.Services;
using Microsoft.EntityFrameworkCore;

namespace CFIssueTracker.Services
{
    public class EFSystemTaskStatusService : EFBaseService, ISystemTaskStatusService
    {       
        public EFSystemTaskStatusService(IDbContextFactory<CFIssueTrackerContext> dbFactory) : base(dbFactory)
        {
            
        }

        public List<SystemTaskStatus> GetAll()
        {            
                return Context.SystemTaskStatus.OrderBy(p => p.Name).ToList();         
        }

        public async Task<List<SystemTaskStatus>> GetAllAsync()
        {            
                return (await Context.SystemTaskStatus.ToListAsync()).OrderBy(p => p.Name).ToList();         
        }

        public async Task<SystemTaskStatus> AddAsync(SystemTaskStatus systemTaskStatus)
        {            
                Context.SystemTaskStatus.Add(systemTaskStatus);
                await Context.SaveChangesAsync();
                return systemTaskStatus;            
        }

        public async Task<SystemTaskStatus> UpdateAsync(SystemTaskStatus systemTaskStatus)
        {            
                Context.SystemTaskStatus.Update(systemTaskStatus);
                await Context.SaveChangesAsync();
                return systemTaskStatus;            
        }

        public async Task<SystemTaskStatus?> GetByIdAsync(string id)
        {            
                var systemTaskStatus = await Context.SystemTaskStatus.FirstOrDefaultAsync(i => i.Id == id);
                return systemTaskStatus;         
        }

        public async Task<List<SystemTaskStatus>> GetByIdsAsync(List<string> ids)
        {            
                return await Context.SystemTaskStatus.Where(i => ids.Contains(i.Id)).ToListAsync();         
        }

        public async Task DeleteByIdAsync(string id)
        {            
                var systemTaskStatus = await Context.SystemTaskStatus.FirstOrDefaultAsync(i => i.Id == id);
                if (systemTaskStatus != null)
                {
                    Context.SystemTaskStatus.Remove(systemTaskStatus);
                    await Context.SaveChangesAsync();
                }         
        }

        public async Task<SystemTaskStatus?> GetByNameAsync(string name)
        {            
                var systemTaskStatus = await Context.SystemTaskStatus.FirstOrDefaultAsync(i => i.Name == name);
                return systemTaskStatus;         
        }
    }
}
