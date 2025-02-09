using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFIssueTrackerCommon.Interfaces
{
    /// <summary>
    /// Request context
    /// </summary>
    public interface IRequestContextService
    {
        /// <summary>
        /// User Id for current request user
        /// </summary>
        public string? UserId { get; }
    }
}
