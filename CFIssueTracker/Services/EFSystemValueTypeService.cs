using CFIssueTrackerCommon.Data;
using CFIssueTrackerCommon.Enums;
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
    public class EFSystemValueTypeService : EFBaseService, ISystemValueTypeService
    {       
        public EFSystemValueTypeService(IDbContextFactory<CFIssueTrackerContext> dbFactory) : base(dbFactory)
        {
            
        }

        public List<SystemValueType> GetAll()
        {            
                return Context.SystemValueType.OrderBy(u => u.Name).ToList();            
        }

        public async Task<List<SystemValueType>> GetAllAsync()
        {            
                return (await Context.SystemValueType.ToListAsync()).OrderBy(u => u.Name).ToList();         
        }

        public async Task<SystemValueType> AddAsync(SystemValueType systemValueType)
        {            
                Context.SystemValueType.Add(systemValueType);
                await Context.SaveChangesAsync();
                return systemValueType;         
        }

        public async Task<SystemValueType> UpdateAsync(SystemValueType systemValueType)
        {            
                Context.SystemValueType.Update(systemValueType);
                await Context.SaveChangesAsync();
                return systemValueType;         
        }

        public async Task<SystemValueType?> GetByIdAsync(string id)
        {
                var systemValueType = await Context.SystemValueType.FirstOrDefaultAsync(i => i.Id == id);
                return systemValueType;         
        }

        public async Task<List<SystemValueType>> GetByIdsAsync(List<string> ids)
        {            
                return await Context.SystemValueType.Where(i => ids.Contains(i.Id)).ToListAsync();         
        }

        public async Task DeleteByIdAsync(string id)
        {            
                var systemValueType = await Context.SystemValueType.FirstOrDefaultAsync(i => i.Id == id);
                if (systemValueType != null)
                {
                    Context.SystemValueType.Remove(systemValueType);
                    await Context.SaveChangesAsync();
                }                        
        }

        public SystemValueType? GetById(string id)
        {            
                var systemValueType = Context.SystemValueType.FirstOrDefault(i => i.Id == id);
                return systemValueType;         
        }

        public async Task<SystemValueType?> GetByNameAsync(string name)
        {            
                var systemValueType = await Context.SystemValueType.FirstOrDefaultAsync(i => i.Name == name);
                return systemValueType;         
        }
    }
}
