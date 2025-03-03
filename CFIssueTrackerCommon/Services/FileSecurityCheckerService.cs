using CFIssueTrackerCommon.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFIssueTrackerCommon.Services
{
    public class FileSecurityCheckerService : IFileSecurityCheckerService
    {
        public Task<bool> ValidateCanUploadImageAsync(byte[] content)
        {
            // TODO: Check file
            return Task.FromResult(true);
        }
    }
}
