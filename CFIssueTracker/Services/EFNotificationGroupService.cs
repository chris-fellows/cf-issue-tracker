using CFIssueTrackerCommon.Data;
using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;
using CFIssueTrackerCommon.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CFIssueTracker.Services
{
    public class EFNotificationGroupService : EFBaseService, INotificationGroupService
    {        
        public EFNotificationGroupService(IDbContextFactory<CFIssueTrackerContext> dbFactory) : base(dbFactory)
        {
            
        }

        public List<NotificationGroup> GetAll()
        {            
                return Context.NotificationGroup
                            .Include(i => i.EmailNotificationConfigs)
                            .OrderBy(i => i.Name).ToList();         
        }

        public async Task<List<NotificationGroup>> GetAllAsync()
        {            
                return (await Context.NotificationGroup
                            .Include(i => i.EmailNotificationConfigs)
                            .ToListAsync()).OrderBy(i => i.Name).ToList();         
        }

        public async Task<NotificationGroup> AddAsync(NotificationGroup notificationGroup)
        {            
                Context.NotificationGroup.Add(notificationGroup);
                await Context.SaveChangesAsync();
                return notificationGroup;            
        }

        public async Task<NotificationGroup> UpdateAsync(NotificationGroup notificationGroup)
        {            
                Context.NotificationGroup.Update(notificationGroup);
                await Context.SaveChangesAsync();
                return notificationGroup;        
        }

        public async Task<NotificationGroup?> GetByIdAsync(string id)
        {            
                var notificationGroup = await Context.NotificationGroup
                            .Include(i => i.EmailNotificationConfigs)
                            .FirstOrDefaultAsync(i => i.Id == id);
                return notificationGroup;         
        }

        public async Task<List<NotificationGroup>> GetByIdsAsync(List<string> ids)
        {            
                return await Context.NotificationGroup
                        .Include(i => i.EmailNotificationConfigs)
                        .Where(i => ids.Contains(i.Id)).ToListAsync();         
        }

        public async Task DeleteByIdAsync(string id)
        {
                var notificationGroup = await Context.NotificationGroup.FirstOrDefaultAsync(i => i.Id == id);
                if (notificationGroup != null)
                {
                    Context.NotificationGroup.Remove(notificationGroup);
                    await Context.SaveChangesAsync();
                }         
        }

        public async Task<NotificationGroup?> GetByNameAsync(string name)
        {            
                var notificationGroup = await Context.NotificationGroup
                                .Include(i => i.EmailNotificationConfigs)
                                .FirstOrDefaultAsync(i => i.Name == name);
                return notificationGroup;         
        }
    }
}
