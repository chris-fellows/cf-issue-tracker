using CFIssueTrackerCommon.Data;
using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;
using Microsoft.EntityFrameworkCore;

namespace CFIssueTracker.Services
{
    public class EFTagService : ITagService
    {
        private readonly IDbContextFactory<CFIssueTrackerContext> _dbFactory;

        public EFTagService(IDbContextFactory<CFIssueTrackerContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public List<Tag> GetAll()
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                return context.Tag.OrderBy(i => i.Name).ToList();
            }
        }

        public async Task<List<Tag>> GetAllAsync()
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                return (await context.Tag.ToListAsync()).OrderBy(i => i.Name).ToList();
            }
        }

        public async Task<Tag> AddAsync(Tag tag)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                context.Tag.Add(tag);
                await context.SaveChangesAsync();
                return tag;
            }
        }

        public async Task<Tag> UpdateAsync(Tag tag)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                context.Tag.Update(tag);
                await context.SaveChangesAsync();
                return tag;
            }
        }

        public async Task<Tag?> GetByIdAsync(string id)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                var tag = await context.Tag.FirstOrDefaultAsync(i => i.Id == id);
                return tag;
            }
        }

        public async Task<List<Tag>> GetByIdsAsync(List<string> ids)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                return await context.Tag.Where(i => ids.Contains(i.Id)).ToListAsync();
            }
        }

        public async Task DeleteByIdAsync(string id)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                var tag = await context.Tag.FirstOrDefaultAsync(i => i.Id == id);
                if (tag != null)
                {
                    context.Tag.Remove(tag);
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task<Tag?> GetByNameAsync(string name)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                var tag = await context.Tag.FirstOrDefaultAsync(i => i.Name == name);
                return tag;
            }
        }
    }
}
