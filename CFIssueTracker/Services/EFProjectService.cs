using CFIssueTrackerCommon.Data;
using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;
using Microsoft.EntityFrameworkCore;

namespace CFIssueTrackerCommon.Services
{
    public class EFProjectService : EFBaseService, IProjectService
    {        
        public EFProjectService(IDbContextFactory<CFIssueTrackerContext> dbFactory) : base(dbFactory)
        {
     
        }

        public List<Project> GetAll()
        {            
                return Context.Project.OrderBy(p => p.Name).ToList();         
        }

        public async Task<List<Project>> GetAllAsync()
        {            
                return (await Context.Project.ToListAsync()).OrderBy(p => p.Name).ToList();         
        }

        public async Task<Project> AddAsync(Project project)
        {            
                Context.Project.Add(project);
                await Context.SaveChangesAsync();
                return project;         
        }

        public async Task<Project> UpdateAsync(Project project)
        {            
                Context.Project.Update(project);
                await Context.SaveChangesAsync();
                return project;         
        }

        public async Task<Project?> GetByIdAsync(string id)
        {
                var project = await Context.Project.FirstOrDefaultAsync(i => i.Id == id);
                return project;         
        }

        public async Task<List<Project>> GetByIdsAsync(List<string> ids)
        {            
                return await Context.Project.Where(i => ids.Contains(i.Id)).ToListAsync();         
        }

        public async Task DeleteByIdAsync(string id)
        {            
                var project = await Context.Project.FirstOrDefaultAsync(i => i.Id == id);
                if (project != null)
                {
                    Context.Project.Remove(project);
                    await Context.SaveChangesAsync();
                }         
        }

        public async Task<Project?> GetByNameAsync(string name)
        {            
                var project = await Context.Project.FirstOrDefaultAsync(i => i.Name == name);
                return project;         
        }
    }
}
