using CFIssueTrackerCommon.Data;
using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CFIssueTracker.Services
{
    public class EFNotificationGroupService : INotificationGroupService
    {
        private readonly IDbContextFactory<CFIssueTrackerContext> _dbFactory;

        public EFNotificationGroupService(IDbContextFactory<CFIssueTrackerContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public List<NotificationGroup> GetAll()
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                return context.NotificationGroup
                            .Include(i => i.EmailNotificationConfigs)
                            .OrderBy(i => i.Name).ToList();
            }
        }

        public async Task<List<NotificationGroup>> GetAllAsync()
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                return (await context.NotificationGroup
                            .Include(i => i.EmailNotificationConfigs)
                            .ToListAsync()).OrderBy(i => i.Name).ToList();
            }
        }

        public async Task<NotificationGroup> AddAsync(NotificationGroup notificationGroup)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                context.NotificationGroup.Add(notificationGroup);
                await context.SaveChangesAsync();
                return notificationGroup;
            }
        }

        public async Task<NotificationGroup> UpdateAsync(NotificationGroup notificationGroup)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                context.NotificationGroup.Update(notificationGroup);
                await context.SaveChangesAsync();
                return notificationGroup;
            }
        }

        public async Task<NotificationGroup?> GetByIdAsync(string id)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                var notificationGroup = await context.NotificationGroup
                            .Include(i => i.EmailNotificationConfigs)
                            .FirstOrDefaultAsync(i => i.Id == id);
                return notificationGroup;
            }
        }

        public async Task<List<NotificationGroup>> GetByIdsAsync(List<string> ids)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                return await context.NotificationGroup
                        .Include(i => i.EmailNotificationConfigs)
                        .Where(i => ids.Contains(i.Id)).ToListAsync();
            }
        }

        public async Task DeleteByIdAsync(string id)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                var notificationGroup = await context.NotificationGroup.FirstOrDefaultAsync(i => i.Id == id);
                if (notificationGroup != null)
                {
                    context.NotificationGroup.Remove(notificationGroup);
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task<NotificationGroup?> GetByNameAsync(string name)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                var notificationGroup = await context.NotificationGroup
                                .Include(i => i.EmailNotificationConfigs)
                                .FirstOrDefaultAsync(i => i.Name == name);
                return notificationGroup;
            }
        }
    }
}
