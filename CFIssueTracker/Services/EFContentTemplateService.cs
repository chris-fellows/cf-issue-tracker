using CFIssueTrackerCommon.Data;
using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;
using Microsoft.EntityFrameworkCore;

namespace CFIssueTracker.Services
{
    public class EFContentTemplateService : IContentTemplateService
    {
        private readonly IDbContextFactory<CFIssueTrackerContext> _dbFactory;

        public EFContentTemplateService(IDbContextFactory<CFIssueTrackerContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public List<ContentTemplate> GetAll()
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                return context.ContentTemplate.OrderBy(e => e.Name).ToList();
            }
        }

        public async Task<List<ContentTemplate>> GetAllAsync()
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                return (await context.ContentTemplate.ToListAsync()).OrderBy(e => e.Name).ToList();
            }
        }

        public async Task<ContentTemplate> AddAsync(ContentTemplate contentTemplate)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                context.ContentTemplate.Add(contentTemplate);
                await context.SaveChangesAsync();
                return contentTemplate;
            }
        }

        public async Task<ContentTemplate> UpdateAsync(ContentTemplate contentTemplate)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                context.ContentTemplate.Update(contentTemplate);
                await context.SaveChangesAsync();
                return contentTemplate;
            }
        }

        public async Task<ContentTemplate?> GetByIdAsync(string id)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                var contentTemplate = await context.ContentTemplate.FirstOrDefaultAsync(i => i.Id == id);
                return contentTemplate;
            }
        }

        public async Task<List<ContentTemplate>> GetByIdsAsync(List<string> ids)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                return await context.ContentTemplate.Where(i => ids.Contains(i.Id)).ToListAsync();
            }
        }

        public async Task DeleteByIdAsync(string id)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                var contentTemplate = await context.ContentTemplate.FirstOrDefaultAsync(i => i.Id == id);
                if (contentTemplate != null)
                {
                    context.ContentTemplate.Remove(contentTemplate);
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task<ContentTemplate?> GetByNameAsync(string name)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                var contentTemplate = await context.ContentTemplate.FirstOrDefaultAsync(i => i.Name == name);
                return contentTemplate;
            }
        }
    }
}
