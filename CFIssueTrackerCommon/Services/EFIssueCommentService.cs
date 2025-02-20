using CFIssueTrackerCommon.Data;
using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;
using Microsoft.EntityFrameworkCore;

namespace CFIssueTracker.Services
{
    public class EFIssueCommentService : IIssueCommentService
    {
        private readonly IDbContextFactory<CFIssueTrackerContext> _dbFactory;

        public EFIssueCommentService(IDbContextFactory<CFIssueTrackerContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public List<IssueComment> GetAll()
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                return context.IssueComment.ToList();
            }
        }

        public async Task<List<IssueComment>> GetAllAsync()
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                return await context.IssueComment.ToListAsync();
            }
        }

        public async Task<IssueComment> AddAsync(IssueComment issueComment)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                context.IssueComment.Add(issueComment);
                await context.SaveChangesAsync();
                return issueComment;
            }
        }

        public async Task<IssueComment> UpdateAsync(IssueComment issueComment)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                context.IssueComment.Update(issueComment);
                await context.SaveChangesAsync();
                return issueComment;
            }
        }

        public async Task<IssueComment?> GetByIdAsync(string id)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                var issueComment = await context.IssueComment.FirstOrDefaultAsync(i => i.Id == id);
                return issueComment;
            }
        }

        public async Task<List<IssueComment>> GetByIssueAsync(string issueId)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                return await context.IssueComment.Where(c => c.IssueId == issueId).ToListAsync();
            }
        }
    }
}
