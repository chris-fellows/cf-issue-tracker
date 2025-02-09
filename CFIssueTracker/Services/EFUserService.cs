using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;
using Microsoft.EntityFrameworkCore;

namespace CFIssueTrackerCommon.Services
{
    public class EFUserService : IUserService
    {
        private readonly IDbContextFactory<CFIssueTracker.Data.CFIssueTrackerContext> _dbFactory;

        public EFUserService(IDbContextFactory<CFIssueTracker.Data.CFIssueTrackerContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<List<User>> GetAllAsync()
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                var users = context.User;
                return await users.ToListAsync();
            }
        }

        public async Task<User> AddAsync(User user)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                context.User.Add(user);
                await context.SaveChangesAsync();
                return user;
            }
        }

        public async Task<User?> GetByIdAsync(string id)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                var user = await context.User.FirstOrDefaultAsync(i => i.Id == id);
                return user;
            }
        }
    }
}
