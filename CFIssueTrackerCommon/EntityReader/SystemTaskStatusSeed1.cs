using CFIssueTrackerCommon.Constants;
using CFIssueTrackerCommon.Models;

namespace CFIssueTrackerCommon.EntityReader
{
    /// <summary>
    /// System task status seed #1
    /// </summary>
    public class SystemTaskStatusSeed1 : IEntityReader<SystemTaskStatus>
    {
        public IEnumerable<SystemTaskStatus> Read()
        {
            var list = new List<SystemTaskStatus>()
            {
                new SystemTaskStatus()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = SystemTaskStatusNames.Pending
                },
                new SystemTaskStatus()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = SystemTaskStatusNames.Active
                },
                new SystemTaskStatus()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = SystemTaskStatusNames.CompletedSuccess
                },
                new SystemTaskStatus()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = SystemTaskStatusNames.CompletedError
                },
            };

            return list;
        }
    }
}
