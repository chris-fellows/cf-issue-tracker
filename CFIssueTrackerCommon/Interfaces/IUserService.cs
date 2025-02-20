using CFIssueTrackerCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFIssueTrackerCommon.Interfaces
{
    public interface IUserService : IEntityWithIdService<User, string>
    {
        User? GetById(string id);

        Task<User?> ValidateCredentialsAsync(string username, string password);
    }
}
