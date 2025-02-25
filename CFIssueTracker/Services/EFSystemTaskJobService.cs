using CFIssueTrackerCommon.Data;
using CFIssueTrackerCommon.Enums;
using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;
using Microsoft.EntityFrameworkCore;

namespace CFIssueTracker.Services
{
    public class EFSystemTaskJobService : ISystemTaskJobService
    {
        private readonly IDbContextFactory<CFIssueTrackerContext> _dbFactory;

        public EFSystemTaskJobService(IDbContextFactory<CFIssueTrackerContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public List<SystemTaskJob> GetAll()
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                return context.SystemTaskJob
                    .Include(i => i.Parameters)
                    .OrderBy(p => p.CreatedDateTime).ToList();
            }
        }

        public async Task<List<SystemTaskJob>> GetAllAsync()
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                return (await context.SystemTaskJob
                    .Include(i => i.Parameters)
                    .ToListAsync()).OrderBy(p => p.CreatedDateTime).ToList();
            }
        }

        public async Task<SystemTaskJob> AddAsync(SystemTaskJob systemTaskJob)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                context.SystemTaskJob.Add(systemTaskJob);
                await context.SaveChangesAsync();
                return systemTaskJob;
            }
        }

        public async Task<SystemTaskJob> UpdateAsync(SystemTaskJob systemTaskJob)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                context.SystemTaskJob.Update(systemTaskJob);
                await context.SaveChangesAsync();
                return systemTaskJob;
            }
        }

        public async Task<SystemTaskJob?> GetByIdAsync(string id)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                var projectComponent = await context.SystemTaskJob
                                .Include(i => i.Parameters)
                                .FirstOrDefaultAsync(i => i.Id == id);
                return projectComponent;
            }
        }

        public async Task<List<SystemTaskJob>> GetByIdsAsync(List<string> ids)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                return await context.SystemTaskJob
                            .Include(i => i.Parameters)
                            .Where(i => ids.Contains(i.Id)).ToListAsync();
            }
        }

        public async Task DeleteByIdAsync(string id)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                var systemTaskJob = await context.SystemTaskJob.FirstOrDefaultAsync(i => i.Id == id);
                if (systemTaskJob != null)
                {
                    context.SystemTaskJob.Remove(systemTaskJob);
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task<List<SystemTaskJob>> GetByFilterAsync(SystemTaskJobFilter filter)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                var systemTaskJobs = await context.SystemTaskJob
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
}
