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
                return context.AuditEvent
                      .Include(i => i.Parameters)
                      .ToList();
            }
        }

        public async Task<List<AuditEvent>> GetAllAsync()
        {
            using (var context = _dbFactory.CreateDbContext())
            {                
                return await context.AuditEvent
                      .Include(i => i.Parameters)
                      .ToListAsync();
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

        public async Task<List<AuditEvent>> GetByIdsAsync(List<string> ids)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                return await context.AuditEvent.Where(i => ids.Contains(i.Id)).ToListAsync();                
            }
        }

        public async Task<List<AuditEvent>> GetByFilterAsync(AuditEventFilter auditEventFilter)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                var auditEvents = await context.AuditEvent
                          .Include(i => i.Parameters)
                          .Where(i =>
                            (
                                auditEventFilter.CreatedDateTimeFrom == null ||
                                i.CreatedDateTime >= auditEventFilter.CreatedDateTimeFrom
                            ) &&
                            (
                                auditEventFilter.CreatedDateTimeTo == null ||
                                i.CreatedDateTime <= auditEventFilter.CreatedDateTimeTo
                            ) &&                          
                            (
                                auditEventFilter.AuditEventTypeIds == null ||
                                !auditEventFilter.AuditEventTypeIds.Any() ||
                                auditEventFilter.AuditEventTypeIds.Contains(i.TypeId)
                            )
                        ).ToListAsync();
                return auditEvents;
            }
        }
    }
}
