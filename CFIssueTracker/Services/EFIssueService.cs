using CFIssueTrackerCommon.Data;
using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;
using Microsoft.EntityFrameworkCore;

namespace CFIssueTrackerCommon.Services
{    
    public class EFIssueService : EFBaseService, IIssueService
    {        
        public EFIssueService(IDbContextFactory<CFIssueTrackerContext> dbFactory) : base(dbFactory)
        {
            
        }

        public List<Issue> GetAll()
        {            
                return Context.Issue
                    .Include(i => i.Documents)
                    .Include(i => i.Tags)
                    .Include(i => i.TrackingUsers)
                    .ToList();         
        }

        public async Task<List<Issue>> GetAllAsync()
        {                 
                return await Context.Issue
                    .Include(i => i.Documents)
                    .Include(i => i.Tags)
                    .Include(i => i.TrackingUsers)
                    .ToListAsync();         
        }

        public async Task<Issue> AddAsync(Issue issue)
        {            
                Context.Issue.Add(issue);
                await Context.SaveChangesAsync();
                return issue;         
        }

        public async Task<Issue> UpdateAsync(Issue issue)
        {                  
                Context.Issue.Update(issue);
                await Context.SaveChangesAsync();
                return issue;         
        }

        public async Task<Issue?> GetByIdAsync(string id)
        {            
                var issue = await Context.Issue
                    .Include(i => i.Documents)
                    .Include(i => i.Tags)
                    .Include(i => i.TrackingUsers)
                    .FirstOrDefaultAsync(i => i.Id == id);
                return issue;            
        }

        public async Task<List<Issue>> GetByIdsAsync(List<string> ids)
        {            
                return await Context.Issue
                    .Include(i => i.Documents)
                    .Include(i => i.Tags)
                    .Include(i => i.TrackingUsers)
                    .Where(i => ids.Contains(i.Id)).ToListAsync();         
        }

        public async Task DeleteByIdAsync(string id)
        {            
                var issue = await Context.Issue
                    .Include(i => i.Documents)
                    .Include(i => i.Tags)
                    .Include(i => i.TrackingUsers)
                    .FirstOrDefaultAsync(i => i.Id == id);
                if (issue != null)
                {
                    Context.Issue.Remove(issue);
                    await Context.SaveChangesAsync();
                }        
        }

        public async Task<List<Issue>> GetByFilterAsync(IssueFilter filter)
        {            
                var issues = await Context.Issue
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
                            ) &&
                            (
                                String.IsNullOrEmpty(filter.ReferencePartial) ||
                                i.Reference.Contains(filter.ReferencePartial)                                   
                            )
                        ).ToListAsync();
                return issues;           
        }

        public List<Issue> GetByFilter(IssueFilter filter)
        {            
                var issues = Context.Issue
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
                            ) &&
                            (
                                String.IsNullOrEmpty(filter.ReferencePartial) ||
                                i.Reference.Contains(filter.ReferencePartial)
                                //EF.Functions.Contains(i.Reference, filter.ReferencePartial)                                
                            )
                        ).ToList();
            
                return issues;            
        }
    }
}
