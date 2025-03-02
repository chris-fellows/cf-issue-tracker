using CFIssueTrackerCommon.Data;
using CFIssueTrackerCommon.Enums;
using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;
using Microsoft.EntityFrameworkCore;
using System.Buffers;

namespace CFIssueTrackerCommon.Services
{
    public class EFAuditEventService : IAuditEventService
    {
        private readonly IAuditEventProcessorService _auditEventProcessorService;
        private readonly IDbContextFactory<CFIssueTrackerContext> _dbFactory;

        public EFAuditEventService(IAuditEventProcessorService auditEventProcessorService,
                                IDbContextFactory<CFIssueTrackerContext> dbFactory)
        {
            _auditEventProcessorService = auditEventProcessorService;
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
                
                await _auditEventProcessorService.ProcessAsync(auditEvent);

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
                var auditEvent = await context.AuditEvent
                            .Include(i => i.Parameters)
                            .FirstOrDefaultAsync(i => i.Id == id);
                return auditEvent;
            }
        }

        public async Task<List<AuditEvent>> GetByIdsAsync(List<string> ids)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                return await context.AuditEvent
                            .Include(i => i.Parameters)
                            .Where(i => ids.Contains(i.Id)).ToListAsync();                
            }
        }

        public async Task DeleteByIdAsync(string id)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                var auditEvent = await context.AuditEvent.FirstOrDefaultAsync(i => i.Id == id);
                if (auditEvent != null)
                {
                    context.AuditEvent.Remove(auditEvent);
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task<List<AuditEvent>> GetByFilterAsync(AuditEventFilter filter)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                var auditEvents = await context.AuditEvent
                          .Include(i => i.Parameters)
                          .Where(i =>
                            (
                                filter.CreatedDateTimeFrom == null ||
                                i.CreatedDateTime >= filter.CreatedDateTimeFrom
                            ) &&
                            (
                                filter.CreatedDateTimeTo == null ||
                                i.CreatedDateTime <= filter.CreatedDateTimeTo
                            ) &&                          
                            (
                                filter.AuditEventTypeIds == null ||
                                !filter.AuditEventTypeIds.Any() ||
                                filter.AuditEventTypeIds.Contains(i.TypeId)
                            ) &&
                            (
                                filter.CreatedUserIds == null ||
                                !filter.CreatedUserIds.Any() ||
                                filter.CreatedUserIds.Contains(i.CreatedUserId)
                            )
                        //(
                        //    filter.Parameters == null ||
                        //    !filter.Parameters.Any() ||
                        //    filter.Parameters.All(fp =>
                        //        i.Parameters.Any(aep =>
                        //                aep.SystemValueTypeId == fp.TypeId &&
                        //                fp.Values.Contains(aep.Value))
                        //            )
                        //)                            
                        ).ToListAsync();

                // Filter on parameters
                if (filter.Parameters != null && filter.Parameters.Any())
                {
                    switch(filter.ParametersLogicalOperator)
                    {
                        case LogicalOperators.And:    // Audit events containing all filter parameter values
                            auditEvents = auditEvents.Where(ae => filter.Parameters.All(fp =>
                                  ae.Parameters.Any(aep =>
                                          aep.SystemValueTypeId == fp.TypeId &&
                                          fp.Values.Contains(aep.Value))
                                  )).ToList();
                            break;
                        case LogicalOperators.Or:   // Audit events containing any filter parameter values
                            auditEvents = auditEvents.Where(ae => filter.Parameters.Any(fp =>
                                ae.Parameters.Any(aep =>
                                        aep.SystemValueTypeId == fp.TypeId &&
                                        fp.Values.Contains(aep.Value))
                                )).ToList();
                            break;
                    }
                }

                return auditEvents;
            }
        }

        public List<AuditEvent> GetByFilter(AuditEventFilter filter)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                var auditEvents = context.AuditEvent
                          .Include(i => i.Parameters)
                          .Where(i =>
                            (
                                filter.CreatedDateTimeFrom == null ||
                                i.CreatedDateTime >= filter.CreatedDateTimeFrom
                            ) &&
                            (
                                filter.CreatedDateTimeTo == null ||
                                i.CreatedDateTime <= filter.CreatedDateTimeTo
                            ) &&
                            (
                                filter.AuditEventTypeIds == null ||
                                !filter.AuditEventTypeIds.Any() ||
                                filter.AuditEventTypeIds.Contains(i.TypeId)
                            ) &&
                            (
                                filter.CreatedUserIds == null ||
                                !filter.CreatedUserIds.Any() ||
                                filter.CreatedUserIds.Contains(i.CreatedUserId)
                            )
                         /*
                         (
                             filter.Parameters == null ||
                             !filter.Parameters.Any() ||
                             filter.Parameters.All(fp =>
                                 i.Parameters.Any(aep => 
                                         aep.SystemValueTypeId == fp.SystemValueTypeId &&
                                         aep.Value == fp.Value)
                                     )                                                                                        
                         )
                         */
                         ).ToList();

                // Filter on parameters
                if (filter.Parameters != null && filter.Parameters.Any())
                {
                    switch (filter.ParametersLogicalOperator)
                    {
                        case LogicalOperators.And:    // Audit events containing all filter parameter values
                            auditEvents = auditEvents.Where(ae => filter.Parameters.All(fp =>
                                  ae.Parameters.Any(aep =>
                                          aep.SystemValueTypeId == fp.TypeId &&
                                          fp.Values.Contains(aep.Value))
                                  )).ToList();
                            break;
                        case LogicalOperators.Or:   // Audit events containing any filter parameter values
                            auditEvents = auditEvents.Where(ae => filter.Parameters.Any(fp =>
                                ae.Parameters.Any(aep =>
                                        aep.SystemValueTypeId == fp.TypeId &&
                                        fp.Values.Contains(aep.Value))
                                )).ToList();
                            break;
                    }
                }

                return auditEvents;
            }
        }     
    }
}
