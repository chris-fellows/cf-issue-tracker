using CFIssueTrackerCommon.Data;
using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;
using Microsoft.EntityFrameworkCore;

namespace CFIssueTrackerCommon.Services
{
    public class EFProjectService : IProjectService
    {
        private readonly IDbContextFactory<CFIssueTrackerContext> _dbFactory;

        public EFProjectService(IDbContextFactory<CFIssueTrackerContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public List<Project> GetAll()
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                return context.Project.OrderBy(p => p.Name).ToList();
            }
        }

        public async Task<List<Project>> GetAllAsync()
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                return (await context.Project.ToListAsync()).OrderBy(p => p.Name).ToList();
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

        public async Task<List<Project>> GetByIdsAsync(List<string> ids)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                return await context.Project.Where(i => ids.Contains(i.Id)).ToListAsync();
            }
        }

        public async Task DeleteByIdAsync(string id)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                var project = await context.Project.FirstOrDefaultAsync(i => i.Id == id);
                if (project != null)
                {
                    context.Project.Remove(project);
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task<Project?> GetByNameAsync(string name)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                var project = await context.Project.FirstOrDefaultAsync(i => i.Name == name);
                return project;
            }
        }
    }
}
