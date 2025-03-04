using CFIssueTrackerCommon.Data;
using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;
using CFIssueTrackerCommon.Services;
using Microsoft.EntityFrameworkCore;

namespace CFIssueTracker.Services
{
    public class EFPasswordResetService : EFBaseService, IPasswordResetService
    {        
        public EFPasswordResetService(IDbContextFactory<CFIssueTrackerContext> dbFactory) : base(dbFactory)
        {
     
        }

        public List<PasswordReset> GetAll()
        {            
                return Context.PasswordReset.OrderBy(i => i.CreatedDateTime).ToList();         
        }

        public async Task<List<PasswordReset>> GetAllAsync()
        {            
                return (await Context.PasswordReset.ToListAsync()).OrderBy(i => i.CreatedDateTime).ToList();      
        }

        public async Task<PasswordReset> AddAsync(PasswordReset passwordReset)
        {            
                Context.PasswordReset.Add(passwordReset);
                await Context.SaveChangesAsync();
                return passwordReset;         
        }

        public async Task<PasswordReset> UpdateAsync(PasswordReset passwordReset)
        {           
                Context.PasswordReset.Update(passwordReset);
                await Context.SaveChangesAsync();
                return passwordReset;         
        }

        public async Task<PasswordReset?> GetByIdAsync(string id)
        {            
                var passwordReset = await Context.PasswordReset.FirstOrDefaultAsync(i => i.Id == id);
                return passwordReset;         
        }

        public async Task<List<PasswordReset>> GetByIdsAsync(List<string> ids)
        {            
                return await Context.PasswordReset.Where(i => ids.Contains(i.Id)).ToListAsync();            
        }

        public async Task DeleteByIdAsync(string id)
        {           
                var passwordReset = await Context.PasswordReset.FirstOrDefaultAsync(i => i.Id == id);
                if (passwordReset != null)
                {
                    Context.PasswordReset.Remove(passwordReset);
                    await Context.SaveChangesAsync();
                }        
        }

        public async Task<PasswordReset?> GetByUserIdAsync(string id)
        {            
                var passwordReset = await Context.PasswordReset.FirstOrDefaultAsync(i => i.UserId == id);
                return passwordReset;         
        }
    }
}
