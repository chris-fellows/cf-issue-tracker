using CFIssueTrackerCommon.Data;
using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;
using CFIssueTrackerCommon.Services;
using Microsoft.EntityFrameworkCore;

namespace CFIssueTracker.Services
{
    public class EFContentTemplateService : EFBaseService, IContentTemplateService
    {        
        public EFContentTemplateService(IDbContextFactory<CFIssueTrackerContext> dbFactory) : base(dbFactory)
        {
    
        }

        public List<ContentTemplate> GetAll()
        {            
                return Context.ContentTemplate.OrderBy(e => e.Name).ToList();            
        }

        public async Task<List<ContentTemplate>> GetAllAsync()
        {            
                return (await Context.ContentTemplate.ToListAsync()).OrderBy(e => e.Name).ToList();         
        }

        public async Task<ContentTemplate> AddAsync(ContentTemplate contentTemplate)
        {            
                Context.ContentTemplate.Add(contentTemplate);
                await Context.SaveChangesAsync();
                return contentTemplate;        
        }

        public async Task<ContentTemplate> UpdateAsync(ContentTemplate contentTemplate)
        {            
                Context.ContentTemplate.Update(contentTemplate);
                await Context.SaveChangesAsync();
                return contentTemplate;         
        }

        public async Task<ContentTemplate?> GetByIdAsync(string id)
        {            
                var contentTemplate = await Context.ContentTemplate.FirstOrDefaultAsync(i => i.Id == id);
                return contentTemplate;      
        }

        public async Task<List<ContentTemplate>> GetByIdsAsync(List<string> ids)
        {            
            return await Context.ContentTemplate.Where(i => ids.Contains(i.Id)).ToListAsync();            
        }

        public async Task DeleteByIdAsync(string id)
        {            
                var contentTemplate = await Context.ContentTemplate.FirstOrDefaultAsync(i => i.Id == id);
                if (contentTemplate != null)
                {
                    Context.ContentTemplate.Remove(contentTemplate);
                    await Context.SaveChangesAsync();
                }         
        }

        public async Task<ContentTemplate?> GetByNameAsync(string name)
        {            
                var contentTemplate = await Context.ContentTemplate.FirstOrDefaultAsync(i => i.Name == name);
                return contentTemplate;         
        }
    }
}
