using CFIssueTrackerCommon.Constants;
using CFIssueTrackerCommon.Models;

namespace CFIssueTrackerCommon.EntityReader
{
    /// <summary>
    /// System task type seed #1
    /// </summary>
    public class SystemTaskTypeSeed1 : IEntityReader<SystemTaskType>
    {
        public IEnumerable<SystemTaskType> Read()
        {
            var list = new List<SystemTaskType>()
            {
                new SystemTaskType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = SystemTaskTypeNames.ManagePasswordResets
                },
                new SystemTaskType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = SystemTaskTypeNames.ManageSystemTaskJobs
                },
                   new SystemTaskType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = SystemTaskTypeNames.SendDatadog
                },
                new SystemTaskType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = SystemTaskTypeNames.SendEmail
                },
                   new SystemTaskType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = SystemTaskTypeNames.SendSlack
                },
                      new SystemTaskType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = SystemTaskTypeNames.SendTeams
                },
            };

            return list;
        }
    }
}
