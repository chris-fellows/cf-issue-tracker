using CFIssueTrackerCommon.Data;
using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;
using Microsoft.EntityFrameworkCore;

namespace CFIssueTrackerCommon.Services
{
    public class EFIssueStatusService : EFBaseService, IIssueStatusService
    {        
        public EFIssueStatusService(IDbContextFactory<CFIssueTrackerContext> dbFactory) : base(dbFactory)
        {
     
        }    

        public List<IssueStatus> GetAll()
        {            
                return Context.IssueStatus.OrderBy(i => i.Name).ToList();         
        }

        public async Task<List<IssueStatus>> GetAllAsync()
        {            
                return (await Context.IssueStatus.ToListAsync()).OrderBy(i => i.Name).ToList();            
        }

        public async Task<IssueStatus> AddAsync(IssueStatus issueStatus)
        {            
                Context.IssueStatus.Add(issueStatus);
                await Context.SaveChangesAsync();
                return issueStatus;         
        }

        public async Task<IssueStatus> UpdateAsync(IssueStatus issueStatus)
        {            
                Context.IssueStatus.Update(issueStatus);
                await Context.SaveChangesAsync();
                return issueStatus;         
        }

        public async Task<IssueStatus?> GetByIdAsync(string id)
        {            
                var issueStatus = await Context.IssueStatus.FirstOrDefaultAsync(i => i.Id == id);
                return issueStatus;            
        }

        public async Task<List<IssueStatus>> GetByIdsAsync(List<string> ids)
        {            
                return await Context.IssueStatus.Where(i => ids.Contains(i.Id)).ToListAsync();         
        }

        public async Task DeleteByIdAsync(string id)
        {            
                var issueStatus = await Context.IssueStatus.FirstOrDefaultAsync(i => i.Id == id);
                if (issueStatus != null)
                {
                    Context.IssueStatus.Remove(issueStatus);
                    await Context.SaveChangesAsync();
                }         
        }

        public async Task<IssueStatus?> GetByNameAsync(string name)
        {            
                var issueStatus = await Context.IssueStatus.FirstOrDefaultAsync(i => i.Name == name);
                return issueStatus;         
        }
    }
}
