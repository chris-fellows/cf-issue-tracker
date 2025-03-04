using CFIssueTrackerCommon.Data;
using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;
using CFIssueTrackerCommon.Services;
using Microsoft.EntityFrameworkCore;

namespace CFIssueTracker.Services
{
    public class EFTagService : EFBaseService, ITagService
    {        
        public EFTagService(IDbContextFactory<CFIssueTrackerContext> dbFactory) : base(dbFactory)
        {
            
        }

        public List<Tag> GetAll()
        {            
                return Context.Tag.OrderBy(i => i.Name).ToList();         
        }

        public async Task<List<Tag>> GetAllAsync()
        {            
                return (await Context.Tag.ToListAsync()).OrderBy(i => i.Name).ToList();         
        }

        public async Task<Tag> AddAsync(Tag tag)
        {            
                Context.Tag.Add(tag);
                await Context.SaveChangesAsync();
                return tag;         
        }

        public async Task<Tag> UpdateAsync(Tag tag)
        {            
                Context.Tag.Update(tag);
                await Context.SaveChangesAsync();
                return tag;         
        }

        public async Task<Tag?> GetByIdAsync(string id)
        {            
                var tag = await Context.Tag.FirstOrDefaultAsync(i => i.Id == id);
                return tag;         
        }

        public async Task<List<Tag>> GetByIdsAsync(List<string> ids)
        {            
                return await Context.Tag.Where(i => ids.Contains(i.Id)).ToListAsync();         
        }

        public async Task DeleteByIdAsync(string id)
        {            
                var tag = await Context.Tag.FirstOrDefaultAsync(i => i.Id == id);
                if (tag != null)
                {
                    Context.Tag.Remove(tag);
                    await Context.SaveChangesAsync();
                }            
        }

        public async Task<Tag?> GetByNameAsync(string name)
        {            
                var tag = await Context.Tag.FirstOrDefaultAsync(i => i.Name == name);
                return tag;         
        }
    }
}
