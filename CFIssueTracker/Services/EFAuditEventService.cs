using CFIssueTrackerCommon.Data;
using CFIssueTrackerCommon.Enums;
using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;
using Microsoft.EntityFrameworkCore;
using System.Buffers;

namespace CFIssueTrackerCommon.Services
{
    public class EFAuditEventService : EFBaseService, IAuditEventService
    {
        private readonly IAuditEventProcessorService _auditEventProcessorService;        

        public EFAuditEventService(IAuditEventProcessorService auditEventProcessorService,
                                IDbContextFactory<CFIssueTrackerContext> dbFactory) : base(dbFactory)
        {
            _auditEventProcessorService = auditEventProcessorService;            
        }

        public List<AuditEvent> GetAll()
        {                        
                return Context.AuditEvent
                      .Include(i => i.Parameters)
                      .ToList();         
        }

        public async Task<List<AuditEvent>> GetAllAsync()
        {            
                return await Context.AuditEvent
                      .Include(i => i.Parameters)
                      .ToListAsync();        
        }

        public async Task<AuditEvent> AddAsync(AuditEvent auditEvent)
        {            
                Context.AuditEvent.Add(auditEvent);
                await Context.SaveChangesAsync();
                
                await _auditEventProcessorService.ProcessAsync(auditEvent);

                return auditEvent;            
        }

        public async Task<AuditEvent> UpdateAsync(AuditEvent auditEvent)
        {            
                Context.AuditEvent.Update(auditEvent);
                await Context.SaveChangesAsync();
                return auditEvent;            
        }

        public async Task<AuditEvent?> GetByIdAsync(string id)
        {            
                var auditEvent = await Context.AuditEvent
                            .Include(i => i.Parameters)
                            .FirstOrDefaultAsync(i => i.Id == id);
                return auditEvent;            
        }

        public async Task<List<AuditEvent>> GetByIdsAsync(List<string> ids)
        {            
                return await Context.AuditEvent
                            .Include(i => i.Parameters)
                            .Where(i => ids.Contains(i.Id)).ToListAsync();                            
        }

        public async Task DeleteByIdAsync(string id)
        {            
                var auditEvent = await Context.AuditEvent.FirstOrDefaultAsync(i => i.Id == id);
                if (auditEvent != null)
                {
                    Context.AuditEvent.Remove(auditEvent);
                    await Context.SaveChangesAsync();
                }       
        }

        public async Task<List<AuditEvent>> GetByFilterAsync(AuditEventFilter filter)
        {            
                var auditEvents = await Context.AuditEvent
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

        public List<AuditEvent> GetByFilter(AuditEventFilter filter)
        {            
                var auditEvents = Context.AuditEvent
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
