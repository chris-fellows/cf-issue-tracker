using CFIssueTrackerCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFIssueTrackerCommon.Interfaces
{
    public interface IIssueService : IEntityWithIdService<Issue, string>
    {
        Task<List<Issue>> GetByFilterAsync(IssueFilter filter);

        List<Issue> GetByFilter(IssueFilter filter);
    }
}
