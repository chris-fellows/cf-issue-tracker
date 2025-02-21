using CFIssueTrackerCommon.Data;
using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;
using Microsoft.EntityFrameworkCore;

namespace CFIssueTrackerCommon.Services
{    
    public class EFIssueService : IIssueService
    {
        private readonly IDbContextFactory<CFIssueTrackerContext> _dbFactory;

        public EFIssueService(IDbContextFactory<CFIssueTrackerContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public List<Issue> GetAll()
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                return context.Issue.ToList();
            }
        }

        public async Task<List<Issue>> GetAllAsync()
        {
            using (var context = _dbFactory.CreateDbContext())
            {                
                return await context.Issue.ToListAsync();
            }
        }

        public async Task<Issue> AddAsync(Issue issue)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                context.Issue.Add(issue);
                await context.SaveChangesAsync();
                return issue;
            }
        }

        public async Task<Issue> UpdateAsync(Issue issue)
        {
            using (var context = _dbFactory.CreateDbContext())
            {                
                context.Issue.Update(issue);
                await context.SaveChangesAsync();
                return issue;
            }
        }

        public async Task<Issue?> GetByIdAsync(string id)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                var issue = await context.Issue.FirstOrDefaultAsync(i => i.Id == id);
                return issue;
            }
        }

        public async Task<List<Issue>> GetByIdsAsync(List<string> ids)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                return await context.Issue.Where(i => ids.Contains(i.Id)).ToListAsync();
            }
        }

        public async Task<List<Issue>> GetByFilterAsync(IssueFilter issueFilter)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                var issues = await context.Issue.Where(i =>
                            (
                                issueFilter.CreatedDateTimeFrom == null ||
                                i.CreatedDateTime >= issueFilter.CreatedDateTimeFrom
                            ) &&
                            (
                                issueFilter.CreatedDateTimeTo == null ||
                                i.CreatedDateTime <= issueFilter.CreatedDateTimeTo
                            ) &&
                            (
                                issueFilter.CreatedUserIds == null ||
                                !issueFilter.CreatedUserIds.Any() ||
                                issueFilter.CreatedUserIds.Contains(i.CreatedUserId)
                            ) &&
                            (
                                issueFilter.ProjectIds == null ||
                                !issueFilter.ProjectIds.Any() ||
                                issueFilter.ProjectIds.Contains(i.ProjectId)
                            ) &&
                            (
                                issueFilter.IssueStatusIds == null ||
                                !issueFilter.IssueStatusIds.Any() ||
                                issueFilter.IssueStatusIds.Contains(i.TypeId)
                            ) &&
                            (
                                issueFilter.IssueTypeIds == null ||
                                !issueFilter.IssueTypeIds.Any() ||
                                issueFilter.IssueTypeIds.Contains(i.TypeId)
                            )
                        ).ToListAsync();
                return issues;
            }
        }
    }
}
