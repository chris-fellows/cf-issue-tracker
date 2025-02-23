using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFIssueTrackerCommon.Interfaces
{
    public interface IEmailConfig
    {
        string Server { get; }

        int Port { get; }

        string Username { get; }

        string Password { get; }
    }
}
