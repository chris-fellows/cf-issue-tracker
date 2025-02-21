using CFIssueTrackerCommon.Data;
using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;
using Microsoft.EntityFrameworkCore;

namespace CFIssueTrackerCommon.Services
{
    public class EFProjectComponentService : IProjectComponentService
    {
        private readonly IDbContextFactory<CFIssueTrackerContext> _dbFactory;

        public EFProjectComponentService(IDbContextFactory<CFIssueTrackerContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public List<ProjectComponent> GetAll()
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                return context.ProjectComponent.OrderBy(p => p.Name).ToList();
            }
        }

        public async Task<List<ProjectComponent>> GetAllAsync()
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                return (await context.ProjectComponent.ToListAsync()).OrderBy(p => p.Name).ToList();
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

        public async Task<List<ProjectComponent>> GetByIdsAsync(List<string> ids)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                return await context.ProjectComponent.Where(i => ids.Contains(i.Id)).ToListAsync();
            }
        }
    }
}
