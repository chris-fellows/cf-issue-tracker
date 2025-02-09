using CFIssueTrackerCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFIssueTrackerCommon.EntityWriter
{
    public class CSVUserWriter : IEntityWriter<User>
    {
        public void Write(IEnumerable<User> users)
        {
            foreach(var user in users)
            {
                Write(user);
            }
        }

        private void Write(User user)
        {

        }
    }
}
