using CFIssueTrackerCommon.Data;
using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;
using Microsoft.EntityFrameworkCore;

namespace CFIssueTrackerCommon.Services
{
    public class EFAuditEventTypeService : IAuditEventTypeService
    {
        private readonly IDbContextFactory<CFIssueTrackerContext> _dbFactory;

        public EFAuditEventTypeService(IDbContextFactory<CFIssueTrackerContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public List<AuditEventType> GetAll()
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                return context.AuditEventType.ToList();
            }
        }

        public async Task<List<AuditEventType>> GetAllAsync()
        {
            using (var context = _dbFactory.CreateDbContext())
            {                
                return await context.AuditEventType.ToListAsync();
            }
        }

        public async Task<AuditEventType> AddAsync(AuditEventType auditEventType)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                context.AuditEventType.Add(auditEventType);
                await context.SaveChangesAsync();
                return auditEventType;
            }
        }

        public async Task<AuditEventType> UpdateAsync(AuditEventType auditEventType)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                context.AuditEventType.Update(auditEventType);
                await context.SaveChangesAsync();
                return auditEventType;
            }
        }

        public async Task<AuditEventType?> GetByIdAsync(string id)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                var auditEventType = await context.AuditEventType.FirstOrDefaultAsync(i => i.Id == id);
                return auditEventType;
            }
        }
    }
}
