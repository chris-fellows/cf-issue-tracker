using CFIssueTrackerCommon.Constants;
using CFIssueTrackerCommon.Models;

namespace CFIssueTrackerCommon.EntityReader
{
    public class SystemValueTypeSeed1 : IEntityReader<SystemValueType>
    {
        public IEnumerable<SystemValueType> Read()
        {
            var list = new List<SystemValueType>()
            {
                new SystemValueType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = SystemValueTypeNames.AuditEventId
                },
                new SystemValueType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = SystemValueTypeNames.AuditEventTypeId
                },
                 new SystemValueType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = SystemValueTypeNames.DatadogCreator
                },
                new SystemValueType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = SystemValueTypeNames.EmailCreator
                },
                new SystemValueType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = SystemValueTypeNames.IssueCommentId
                },
                new SystemValueType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = SystemValueTypeNames.IssueId
                },
                new SystemValueType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = SystemValueTypeNames.IssueStatusId
                },
                new SystemValueType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = SystemValueTypeNames.IssueTypeId
                },
                new SystemValueType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = SystemValueTypeNames.MetricsTypeId
                },
                new SystemValueType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = SystemValueTypeNames.PasswordResetId
                },
                new SystemValueType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = SystemValueTypeNames.ProjectId
                },
                new SystemValueType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = SystemValueTypeNames.ProjectComponentId
                },
                new SystemValueType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = SystemValueTypeNames.RecipientEmails
                },
                new SystemValueType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = SystemValueTypeNames.SlackCreator
                },
                new SystemValueType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = SystemValueTypeNames.SystemValueTypeId
                },
                 new SystemValueType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = SystemValueTypeNames.TeamsCreator
                },
                new SystemValueType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = SystemValueTypeNames.UserId
                }
            };

            return list;
        }
    }
}
