using CFIssueTrackerCommon.Data;
using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;
using CFIssueTrackerCommon.Services;
using Microsoft.EntityFrameworkCore;

namespace CFIssueTracker.Services
{
    public class EFSystemTaskTypeService : EFBaseService, ISystemTaskTypeService
    {        
        public EFSystemTaskTypeService(IDbContextFactory<CFIssueTrackerContext> dbFactory) : base(dbFactory)
        {
            
        }

        public List<SystemTaskType> GetAll()
        {            
                return Context.SystemTaskType.OrderBy(p => p.Name).ToList();         
        }

        public async Task<List<SystemTaskType>> GetAllAsync()
        {           
                return (await Context.SystemTaskType.ToListAsync()).OrderBy(p => p.Name).ToList();         
        }

        public async Task<SystemTaskType> AddAsync(SystemTaskType systemTaskType)
        {            
                Context.SystemTaskType.Add(systemTaskType);
                await Context.SaveChangesAsync();
                return systemTaskType;         
        }

        public async Task<SystemTaskType> UpdateAsync(SystemTaskType systemTaskType)
        {            
                Context.SystemTaskType.Update(systemTaskType);
                await Context.SaveChangesAsync();
                return systemTaskType;         
        }

        public async Task<SystemTaskType?> GetByIdAsync(string id)
        {            
                var project = await Context.SystemTaskType.FirstOrDefaultAsync(i => i.Id == id);
                return project;         
        }

        public async Task<List<SystemTaskType>> GetByIdsAsync(List<string> ids)
        {            
                return await Context.SystemTaskType.Where(i => ids.Contains(i.Id)).ToListAsync();            
        }

        public async Task DeleteByIdAsync(string id)
        {            
                var systemTaskType = await Context.SystemTaskType.FirstOrDefaultAsync(i => i.Id == id);
                if (systemTaskType != null)
                {
                    Context.SystemTaskType.Remove(systemTaskType);
                    await Context.SaveChangesAsync();
                }         
        }

        public async Task<SystemTaskType?> GetByNameAsync(string name)
        {            
                var systemTaskType = await Context.SystemTaskType.FirstOrDefaultAsync(i => i.Name == name);
                return systemTaskType;         
        }
    }
}
