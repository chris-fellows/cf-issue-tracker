using CFIssueTrackerCommon.Data;
using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;
using Microsoft.EntityFrameworkCore;

namespace CFIssueTrackerCommon.Services
{
    public class EFIssueTypeService : EFBaseService, IIssueTypeService
    {        
        public EFIssueTypeService(IDbContextFactory<CFIssueTrackerContext> dbFactory) : base(dbFactory)
        {
     
        }

        public List<IssueType> GetAll()
        {            
                return Context.IssueType.OrderBy(i => i.Name).ToList();         
        }

        public async Task<List<IssueType>> GetAllAsync()
        {            
                return (await Context.IssueType.ToListAsync()).OrderBy(i => i.Name).ToList();        
        }

        public async Task<IssueType> AddAsync(IssueType issueType)
        {            
                Context.IssueType.Add(issueType);
                await Context.SaveChangesAsync();
                return issueType;            
        }

        public async Task<IssueType> UpdateAsync(IssueType issueType)
        {            
                Context.IssueType.Update(issueType);
                await Context.SaveChangesAsync();
                return issueType;            
        }

        public async Task<IssueType?> GetByIdAsync(string id)
        {            
                var issueType = await Context.IssueType.FirstOrDefaultAsync(i => i.Id == id);
                return issueType;            
        }

        public async Task<List<IssueType>> GetByIdsAsync(List<string> ids)
        {            
                return await Context.IssueType.Where(i => ids.Contains(i.Id)).ToListAsync();         
        }

        public async Task DeleteByIdAsync(string id)
        {            
                var issueType = await Context.IssueType.FirstOrDefaultAsync(i => i.Id == id);
                if (issueType != null)
                {
                    Context.IssueType.Remove(issueType);
                    await Context.SaveChangesAsync();
                }            
        }

        public async Task<IssueType?> GetByNameAsync(string name)
        {            
                var issueType = await Context.IssueType.FirstOrDefaultAsync(i => i.Name == name);
                return issueType;         
        }
    }
}
