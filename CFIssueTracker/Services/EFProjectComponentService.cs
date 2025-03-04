using CFIssueTrackerCommon.Data;
using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;
using Microsoft.EntityFrameworkCore;

namespace CFIssueTrackerCommon.Services
{
    public class EFProjectComponentService : EFBaseService, IProjectComponentService
    {        
        public EFProjectComponentService(IDbContextFactory<CFIssueTrackerContext> dbFactory) : base(dbFactory)
        {
            
        }

        public List<ProjectComponent> GetAll()
        {            
                return Context.ProjectComponent.OrderBy(p => p.Name).ToList();         
        }

        public async Task<List<ProjectComponent>> GetAllAsync()
        {            
                return (await Context.ProjectComponent.ToListAsync()).OrderBy(p => p.Name).ToList();         
        }

        public async Task<ProjectComponent> AddAsync(ProjectComponent projectComponent)
        {            
                Context.ProjectComponent.Add(projectComponent);
                await Context.SaveChangesAsync();
                return projectComponent;         
        }


        public async Task<ProjectComponent> UpdateAsync(ProjectComponent projectComponent)
        {            
                Context.ProjectComponent.Update(projectComponent);
                await Context.SaveChangesAsync();
                return projectComponent;         
        }

        public async Task<ProjectComponent?> GetByIdAsync(string id)
        {            
                var projectComponent = await Context.ProjectComponent.FirstOrDefaultAsync(i => i.Id == id);
                return projectComponent;         
        }

        public async Task<List<ProjectComponent>> GetByIdsAsync(List<string> ids)
        {            
                return await Context.ProjectComponent.Where(i => ids.Contains(i.Id)).ToListAsync();         
        }

        public async Task DeleteByIdAsync(string id)
        {            
                var projectComponent = await Context.ProjectComponent.FirstOrDefaultAsync(i => i.Id == id);
                if (projectComponent != null)
                {
                    Context.ProjectComponent.Remove(projectComponent);
                    await Context.SaveChangesAsync();
                }         
        }

        public async Task<ProjectComponent?> GetByNameAsync(string name)
        {            
                var projectComponent = await Context.ProjectComponent.FirstOrDefaultAsync(i => i.Name == name);
                return projectComponent;         
        }
    }
}
