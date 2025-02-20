using CFIssueTrackerCommon.Data;
using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;
using Microsoft.EntityFrameworkCore;

namespace CFIssueTrackerCommon.Services
{
    public class EFAuditEventService : IAuditEventService
    {
        private readonly IDbContextFactory<CFIssueTrackerContext> _dbFactory;

        public EFAuditEventService(IDbContextFactory<CFIssueTrackerContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public List<AuditEvent> GetAll()
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                return context.AuditEvent.ToList();
            }
        }

        public async Task<List<AuditEvent>> GetAllAsync()
        {
            using (var context = _dbFactory.CreateDbContext())
            {                
                return await context.AuditEvent.ToListAsync();
            }
        }

        public async Task<AuditEvent> AddAsync(AuditEvent auditEvent)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                context.AuditEvent.Add(auditEvent);
                await context.SaveChangesAsync();
                return auditEvent;
            }
        }

        public async Task<AuditEvent> UpdateAsync(AuditEvent auditEvent)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                context.AuditEvent.Update(auditEvent);
                await context.SaveChangesAsync();
                return auditEvent;
            }
        }

        public async Task<AuditEvent?> GetByIdAsync(string id)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                var auditEvent = await context.AuditEvent.FirstOrDefaultAsync(i => i.Id == id);
                return auditEvent;
            }
        }
    }
}
