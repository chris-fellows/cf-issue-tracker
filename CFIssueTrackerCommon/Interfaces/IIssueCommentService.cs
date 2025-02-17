using CFIssueTrackerCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFIssueTrackerCommon.Interfaces
{
    public interface IIssueCommentService : IEntityWithIdService<IssueComment, string>
    {
        Task<List<IssueComment>> GetByIssueAsync(string issueId);
    }
}
