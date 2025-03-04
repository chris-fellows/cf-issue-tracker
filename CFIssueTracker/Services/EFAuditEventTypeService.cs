using CFIssueTrackerCommon.Data;
using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;
using Microsoft.EntityFrameworkCore;

namespace CFIssueTrackerCommon.Services
{
    public class EFAuditEventTypeService : EFBaseService, IAuditEventTypeService
    {        
        public EFAuditEventTypeService(IDbContextFactory<CFIssueTrackerContext> dbFactory) : base(dbFactory)
        {
            
        }

        public List<AuditEventType> GetAll()
        {            
                return Context.AuditEventType
                            .Include(e => e.NotificationGroups)
                             .OrderBy(e => e.Name).ToList();            
        }

        public async Task<List<AuditEventType>> GetAllAsync()
        { 
                return (await Context.AuditEventType
                            .Include(e => e.NotificationGroups)
                            .ToListAsync()).OrderBy(e => e.Name).ToList();          
        }

        public async Task<AuditEventType> AddAsync(AuditEventType auditEventType)
        {            
                Context.AuditEventType.Add(auditEventType);
                await Context.SaveChangesAsync();
                return auditEventType;            
        }

        public async Task<AuditEventType> UpdateAsync(AuditEventType auditEventType)
        {            
                Context.AuditEventType.Update(auditEventType);
                await Context.SaveChangesAsync();
                return auditEventType;            
        }

        public async Task<AuditEventType?> GetByIdAsync(string id)
        {            
                var auditEventType = await Context.AuditEventType
                            .Include(e => e.NotificationGroups)
                            .FirstOrDefaultAsync(i => i.Id == id);
                return auditEventType;         
        }

        public async Task<List<AuditEventType>> GetByIdsAsync(List<string> ids)
        {            
            return await Context.AuditEventType
                    .Include(e => e.NotificationGroups)
                    .Where(i => ids.Contains(i.Id))
                    .ToListAsync();                            
        }

        public async Task DeleteByIdAsync(string id)
        {            
                var auditEventType = await Context.AuditEventType.FirstOrDefaultAsync(i => i.Id == id);
                if (auditEventType != null)
                {
                    Context.AuditEventType.Remove(auditEventType);
                    await Context.SaveChangesAsync();
                }         
        }

        public async Task<AuditEventType?> GetByNameAsync(string name)
        {           
                var auditEventType = await Context.AuditEventType
                                .Include(e => e.NotificationGroups)
                                .FirstOrDefaultAsync(i => i.Name == name);
                return auditEventType;         
        }
    }
}
