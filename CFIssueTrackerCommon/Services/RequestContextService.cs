using CFIssueTrackerCommon.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFIssueTrackerCommon.Services
{
    public class RequestContextService : IRequestContextService
    {
        public string? UserId
        {
            get { return Guid.NewGuid().ToString(); }       // TODO: Return current User Id
        }
    }
}
