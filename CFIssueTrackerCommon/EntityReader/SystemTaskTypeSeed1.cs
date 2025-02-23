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
                    Name = SystemTaskTypeNames.SendIssueAssignedEmail
                },
                new SystemTaskType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = SystemTaskTypeNames.SendNewUserEmail
                },
                new SystemTaskType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = SystemTaskTypeNames.SendResetPasswordEmail
                }
            };

            return list;
        }
    }
}
