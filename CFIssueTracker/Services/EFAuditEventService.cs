using CFIssueTracker.Data;
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
    public class EFAuditEventService : IAuditEventService
    {
        private readonly IDbContextFactory<CFIssueTracker.Data.CFIssueTrackerContext> _dbFactory;

        public EFAuditEventService(IDbContextFactory<CFIssueTracker.Data.CFIssueTrackerContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<List<AuditEvent>> GetAllAsync()
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                var auditEvents = context.AuditEvent;
                return await auditEvents.ToListAsync();
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
