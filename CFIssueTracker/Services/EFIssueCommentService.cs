using CFIssueTrackerCommon.Data;
using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;
using CFIssueTrackerCommon.Services;
using Microsoft.EntityFrameworkCore;

namespace CFIssueTracker.Services
{
    public class EFIssueCommentService : EFBaseService, IIssueCommentService
    {        
        public EFIssueCommentService(IDbContextFactory<CFIssueTrackerContext> dbFactory) : base(dbFactory)
        {
     
        }

        public List<IssueComment> GetAll()
        {            
                return Context.IssueComment.ToList();            
        }

        public async Task<List<IssueComment>> GetAllAsync()
        {            
                return await Context.IssueComment.ToListAsync();         
        }

        public async Task<IssueComment> AddAsync(IssueComment issueComment)
        {            
                Context.IssueComment.Add(issueComment);
                await Context.SaveChangesAsync();
                return issueComment;            
        }

        public async Task<IssueComment> UpdateAsync(IssueComment issueComment)
        {            
                Context.IssueComment.Update(issueComment);
                await Context.SaveChangesAsync();
                return issueComment;         
        }

        public async Task<IssueComment?> GetByIdAsync(string id)
        {            
                var issueComment = await Context.IssueComment.FirstOrDefaultAsync(i => i.Id == id);
                return issueComment;         
        }

        public async Task<List<IssueComment>> GetByIdsAsync(List<string> ids)
        {            
                return await Context.IssueComment.Where(i => ids.Contains(i.Id)).ToListAsync();         
        }

        public async Task DeleteByIdAsync(string id)
        {            
                var issueComment = await Context.IssueComment.FirstOrDefaultAsync(i => i.Id == id);
                if (issueComment != null)
                {
                    Context.IssueComment.Remove(issueComment);
                    await Context.SaveChangesAsync();
                }         
        }

        public async Task<List<IssueComment>> GetByIssueAsync(string issueId)
        {            
                return await Context.IssueComment.Where(c => c.IssueId == issueId).ToListAsync();         
        }
    }
}
