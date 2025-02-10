using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;
using Microsoft.EntityFrameworkCore;

namespace CFIssueTrackerCommon.Services
{
    public class EFProjectService : IProjectService
    {
        private readonly IDbContextFactory<CFIssueTracker.Data.CFIssueTrackerContext> _dbFactory;

        public EFProjectService(IDbContextFactory<CFIssueTracker.Data.CFIssueTrackerContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<List<Project>> GetAllAsync()
        {
            using (var context = _dbFactory.CreateDbContext())
            {                
                return await context.Project.ToListAsync();
            }
        }

        public async Task<Project> AddAsync(Project project)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                context.Project.Add(project);
                await context.SaveChangesAsync();
                return project;
            }
        }

        public async Task<Project> UpdateAsync(Project project)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                context.Project.Update(project);
                await context.SaveChangesAsync();
                return project;
            }
        }

        public async Task<Project?> GetByIdAsync(string id)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                var project = await context.Project.FirstOrDefaultAsync(i => i.Id == id);
                return project;
            }
        }
    }
}
