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
                return context.Issue
                    .Include(i => i.Documents)
                    .Include(i => i.Tags)
                    .Include(i => i.TrackingUsers)
                    .ToList();
            }
        }

        public async Task<List<Issue>> GetAllAsync()
        {
            using (var context = _dbFactory.CreateDbContext())
            {                
                return await context.Issue
                    .Include(i => i.Documents)
                    .Include(i => i.Tags)
                    .Include(i => i.TrackingUsers)
                    .ToListAsync();
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
                var issue = await context.Issue
                    .Include(i => i.Documents)
                    .Include(i => i.Tags)
                    .Include(i => i.TrackingUsers)
                    .FirstOrDefaultAsync(i => i.Id == id);
                return issue;
            }
        }

        public async Task<List<Issue>> GetByIdsAsync(List<string> ids)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                return await context.Issue
                    .Include(i => i.Documents)
                    .Include(i => i.Tags)
                    .Include(i => i.TrackingUsers)
                    .Where(i => ids.Contains(i.Id)).ToListAsync();
            }
        }

        public async Task DeleteByIdAsync(string id)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                var issue = await context.Issue
                    .Include(i => i.Documents)
                    .Include(i => i.Tags)
                    .Include(i => i.TrackingUsers)
                    .FirstOrDefaultAsync(i => i.Id == id);
                if (issue != null)
                {
                    context.Issue.Remove(issue);
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task<List<Issue>> GetByFilterAsync(IssueFilter filter)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                var issues = await context.Issue
                    .Include(i => i.Documents)
                    .Include(i => i.Tags)
                    .Include(i => i.TrackingUsers)
                    .Where(i =>
                            (
                                filter.CreatedDateTimeFrom == null ||
                                i.CreatedDateTime >= filter.CreatedDateTimeFrom
                            ) &&
                            (
                                filter.CreatedDateTimeTo == null ||
                                i.CreatedDateTime <= filter.CreatedDateTimeTo
                            ) &&
                            (
                                filter.AssignedUserIds == null ||
                                !filter.AssignedUserIds.Any() ||
                                filter.AssignedUserIds.Contains(i.AssignedUserId)
                            ) &&
                            (
                                filter.CreatedUserIds == null ||
                                !filter.CreatedUserIds.Any() ||
                                filter.CreatedUserIds.Contains(i.CreatedUserId)
                            ) &&
                            (
                                filter.ProjectIds == null ||
                                !filter.ProjectIds.Any() ||
                                filter.ProjectIds.Contains(i.ProjectId)
                            ) &&
                            (
                                filter.IssueStatusIds == null ||
                                !filter.IssueStatusIds.Any() ||
                                filter.IssueStatusIds.Contains(i.StatusId)
                            ) &&
                            (
                                filter.IssueTypeIds == null ||
                                !filter.IssueTypeIds.Any() ||
                                filter.IssueTypeIds.Contains(i.TypeId)
                            )
                        ).ToListAsync();
                return issues;
            }
        }

        public List<Issue> GetByFilter(IssueFilter filter)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                var issues = context.Issue
                    .Include(i => i.Documents)
                    .Include(i => i.Tags)
                    .Include(i => i.TrackingUsers).
                    Where(i =>
                            (
                                filter.CreatedDateTimeFrom == null ||
                                i.CreatedDateTime >= filter.CreatedDateTimeFrom
                            ) &&
                            (
                                filter.CreatedDateTimeTo == null ||
                                i.CreatedDateTime <= filter.CreatedDateTimeTo
                            ) &&
                            (
                                filter.AssignedUserIds == null ||
                                !filter.AssignedUserIds.Any() ||
                                filter.AssignedUserIds.Contains(i.AssignedUserId)
                            ) &&
                            (
                                filter.CreatedUserIds == null ||
                                !filter.CreatedUserIds.Any() ||
                                filter.CreatedUserIds.Contains(i.CreatedUserId)
                            ) &&
                            (
                                filter.ProjectIds == null ||
                                !filter.ProjectIds.Any() ||
                                filter.ProjectIds.Contains(i.ProjectId)
                            ) &&
                            (
                                filter.IssueStatusIds == null ||
                                !filter.IssueStatusIds.Any() ||
                                filter.IssueStatusIds.Contains(i.StatusId)
                            ) &&
                            (
                                filter.IssueTypeIds == null ||
                                !filter.IssueTypeIds.Any() ||
                                filter.IssueTypeIds.Contains(i.TypeId)
                            )
                        ).ToList();
                return issues;
            }
        }
    }
}
