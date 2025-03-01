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
                    Name = SystemTaskTypeNames.SendEmail
                }             
            };

            return list;
        }
    }
}
