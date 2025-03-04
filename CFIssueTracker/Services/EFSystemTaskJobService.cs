using CFIssueTrackerCommon.Data;
using CFIssueTrackerCommon.Enums;
using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;
using CFIssueTrackerCommon.Services;
using Microsoft.EntityFrameworkCore;

namespace CFIssueTracker.Services
{
    public class EFSystemTaskJobService : EFBaseService, ISystemTaskJobService
    {        
        public EFSystemTaskJobService(IDbContextFactory<CFIssueTrackerContext> dbFactory) : base(dbFactory)
        {
            
        }

        public List<SystemTaskJob> GetAll()
        {            
                return Context.SystemTaskJob
                    .Include(i => i.Parameters)
                    .OrderBy(p => p.CreatedDateTime).ToList();         
        }

        public async Task<List<SystemTaskJob>> GetAllAsync()
        {            
                return (await Context.SystemTaskJob
                    .Include(i => i.Parameters)
                    .ToListAsync()).OrderBy(p => p.CreatedDateTime).ToList();         
        }

        public async Task<SystemTaskJob> AddAsync(SystemTaskJob systemTaskJob)
        {            
                Context.SystemTaskJob.Add(systemTaskJob);
                await Context.SaveChangesAsync();
                return systemTaskJob;         
        }

        public async Task<SystemTaskJob> UpdateAsync(SystemTaskJob systemTaskJob)
        {
            
                Context.SystemTaskJob.Update(systemTaskJob);
                await Context.SaveChangesAsync();
                return systemTaskJob;            
        }

        public async Task<SystemTaskJob?> GetByIdAsync(string id)
        {            
                var projectComponent = await Context.SystemTaskJob
                                .Include(i => i.Parameters)
                                .FirstOrDefaultAsync(i => i.Id == id);
                return projectComponent;         
        }

        public async Task<List<SystemTaskJob>> GetByIdsAsync(List<string> ids)
        {            
                return await Context.SystemTaskJob
                            .Include(i => i.Parameters)
                            .Where(i => ids.Contains(i.Id)).ToListAsync();         
        }

        public async Task DeleteByIdAsync(string id)
        {            
                var systemTaskJob = await Context.SystemTaskJob.FirstOrDefaultAsync(i => i.Id == id);
                if (systemTaskJob != null)
                {
                    Context.SystemTaskJob.Remove(systemTaskJob);
                    await Context.SaveChangesAsync();
                }         
        }

        public async Task<List<SystemTaskJob>> GetByFilterAsync(SystemTaskJobFilter filter)
        {            
                var systemTaskJobs = await Context.SystemTaskJob
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
                                filter.TypeIds == null ||
                                !filter.TypeIds.Any() ||
                                filter.TypeIds.Contains(i.TypeId)
                            ) &&
                            (
                                filter.StatusIds == null ||
                                !filter.StatusIds.Any() ||
                                filter.StatusIds.Contains(i.StatusId)
                            )
                        ).ToListAsync();

                // Filter on parameters
                if (filter.Parameters != null && filter.Parameters.Any())
                {
                    switch (filter.ParametersLogicalOperator)
                    {
                        case LogicalOperators.And:    // Jobs containing all filter parameter values
                            systemTaskJobs = systemTaskJobs.Where(j => filter.Parameters.All(fp =>
                                  j.Parameters.Any(jp =>
                                          jp.SystemValueTypeId == fp.TypeId &&
                                          fp.Values.Contains(jp.Value))
                                  )).ToList();
                            break;
                        case LogicalOperators.Or:   // Jobs containing any filter parameter values
                            systemTaskJobs = systemTaskJobs.Where(j => filter.Parameters.Any(fp =>
                                j.Parameters.Any(jp =>
                                        jp.SystemValueTypeId == fp.TypeId &&
                                        fp.Values.Contains(jp.Value))
                                )).ToList();
                            break;
                    }
                }

                return systemTaskJobs;
        }        
    }
}
