using CFIssueTrackerCommon.Data;
using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;
using CFIssueTrackerCommon.Services;
using Microsoft.EntityFrameworkCore;

namespace CFIssueTracker.Services
{
    public class EFDocumentService : EFBaseService, IDocumentService
    {        
        public EFDocumentService(IDbContextFactory<CFIssueTrackerContext> dbFactory) : base(dbFactory)
        {
   
        }

        public List<Document> GetAll()
        {            
            return Context.Document.OrderBy(i => i.Name).ToList();         
        }

        public async Task<List<Document>> GetAllAsync()
        {            
                return (await Context.Document.ToListAsync()).OrderBy(i => i.Name).ToList();         
        }

        public async Task<Document> AddAsync(Document document)
        {            
                Context.Document.Add(document);
                await Context.SaveChangesAsync();
                return document;            
        }

        public async Task<Document> UpdateAsync(Document document)
        {           
                Context.Document.Update(document);
                await Context.SaveChangesAsync();
                return document;            
        }

        public async Task<Document?> GetByIdAsync(string id)
        {            
                var document = await Context.Document.FirstOrDefaultAsync(i => i.Id == id);
                return document;         
        }

        public async Task<List<Document>> GetByIdsAsync(List<string> ids)
        {            
                return await Context.Document.Where(i => ids.Contains(i.Id)).ToListAsync();            
        }

        public async Task DeleteByIdAsync(string id)
        {           
                var document = await Context.Document.FirstOrDefaultAsync(i => i.Id == id);
                if (document != null)
                {
                    Context.Document.Remove(document);
                    await Context.SaveChangesAsync();
                }         
        }

        public async Task<Document?> GetByNameAsync(string name)
        {            
                var document = await Context.Document.FirstOrDefaultAsync(i => i.Name == name);
                return document;            
        }
    }
}
