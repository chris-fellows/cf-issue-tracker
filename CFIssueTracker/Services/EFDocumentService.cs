using CFIssueTrackerCommon.Data;
using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;
using Microsoft.EntityFrameworkCore;

namespace CFIssueTracker.Services
{
    public class EFDocumentService : IDocumentService
    {
        private readonly IDbContextFactory<CFIssueTrackerContext> _dbFactory;

        public EFDocumentService(IDbContextFactory<CFIssueTrackerContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public List<Document> GetAll()
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                return context.Document.OrderBy(i => i.Name).ToList();
            }
        }

        public async Task<List<Document>> GetAllAsync()
        {
            //var context = GetContext();
            //return await context.IssueStatus.ToListAsync();

            using (var context = _dbFactory.CreateDbContext())
            {
                return (await context.Document.ToListAsync()).OrderBy(i => i.Name).ToList();
            }
        }

        public async Task<Document> AddAsync(Document document)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                context.Document.Add(document);
                await context.SaveChangesAsync();
                return document;
            }
        }

        public async Task<Document> UpdateAsync(Document document)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                context.Document.Update(document);
                await context.SaveChangesAsync();
                return document;
            }
        }

        public async Task<Document?> GetByIdAsync(string id)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                var document = await context.Document.FirstOrDefaultAsync(i => i.Id == id);
                return document;
            }
        }

        public async Task<List<Document>> GetByIdsAsync(List<string> ids)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                return await context.Document.Where(i => ids.Contains(i.Id)).ToListAsync();
            }
        }

        public async Task DeleteByIdAsync(string id)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                var document = await context.Document.FirstOrDefaultAsync(i => i.Id == id);
                if (document != null)
                {
                    context.Document.Remove(document);
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task<Document?> GetByNameAsync(string name)
        {
            using (var context = _dbFactory.CreateDbContext())
            {
                var document = await context.Document.FirstOrDefaultAsync(i => i.Name == name);
                return document;
            }
        }
    }
}
