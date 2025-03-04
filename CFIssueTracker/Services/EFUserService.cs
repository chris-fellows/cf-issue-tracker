using CFIssueTrackerCommon.Data;
using CFIssueTrackerCommon.Enums;
using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;
using Microsoft.EntityFrameworkCore;

namespace CFIssueTrackerCommon.Services
{
    public class EFUserService : EFBaseService, IUserService
    {        
        private readonly IPasswordService _passwordService;

        public EFUserService(IDbContextFactory<CFIssueTrackerContext> dbFactory,
                            IPasswordService passwordService) : base(dbFactory)
        {     
            _passwordService = passwordService;
        }

        public List<User> GetAll()
        {            
                return Context.User.OrderBy(u => u.Name).ToList();         
        }

        public async Task<List<User>> GetAllAsync()
        {            
                return (await Context.User.ToListAsync()).OrderBy(u => u.Name).ToList();       
        }

        public async Task<User> AddAsync(User user)
        {            
                Context.User.Add(user);
                await Context.SaveChangesAsync();
                return user;         
        }

        public async Task<User> UpdateAsync(User user)
        {            
                Context.User.Update(user);
                await Context.SaveChangesAsync();
                return user;         
        }

        public async Task<User?> GetByIdAsync(string id)
        {            
                var user = await Context.User.FirstOrDefaultAsync(i => i.Id == id);
                return user;         
        }

        public async Task<List<User>> GetByIdsAsync(List<string> ids)
        {            
                return await Context.User.Where(i => ids.Contains(i.Id)).ToListAsync();         
        }

        public async Task DeleteByIdAsync(string id)
        {            
                var user = await Context.User.FirstOrDefaultAsync(i => i.Id == id);
                if (user != null)
                {
                    Context.User.Remove(user);
                    await Context.SaveChangesAsync();
                }         
        }

        public async Task<User?> ValidateCredentialsAsync(string username, string password)                                                        
        {            
                var user = await Context.User.FirstOrDefaultAsync(i => i.Email == username);
                if (user != null &&
                    user.Active &&
                    user.GetUserType() == UserTypes.Normal &&
                    _passwordService.IsValid(user.Password, password, user.Salt))                    
                {
                    return user;
                }
                return null;         
        }

        public User? GetById(string id)
        {            
            var user = Context.User.FirstOrDefault(i => i.Id == id);
            return user;         
        }
    }
}
