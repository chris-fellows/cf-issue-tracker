using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;
using Microsoft.EntityFrameworkCore;

namespace CFIssueTrackerCommon.Services
{
    public class EFProjectComponentService : IProjectComponentService
    {
        private readonly IDbContextFactory<CFIssueTracker.Data.CFIssueTrackerContext> _dbFactory;

        public EFProjectComponentService(IDbContextFactory<CFIssueTracker.Data.CFIssueTrackerContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<List<ProjectComponent>> GetAllAsync()
        {
            using (var context = _dbFactory.CreateDbContext())
            {                
                return await context.ProjectComponent.ToListAsync();
            }
        }

        public async Task<ProjectComponent> AddAsync(ProjectComponent projectComponent)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                context.ProjectComponent.Add(projectComponent);
                await context.SaveChangesAsync();
                return projectComponent;
            }
        }


        public async Task<ProjectComponent> UpdateAsync(ProjectComponent projectComponent)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                context.ProjectComponent.Update(projectComponent);
                await context.SaveChangesAsync();
                return projectComponent;
            }
        }

        public async Task<ProjectComponent?> GetByIdAsync(string id)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                var projectComponent = await context.ProjectComponent.FirstOrDefaultAsync(i => i.Id == id);
                return projectComponent;
            }
        }
    }
}
