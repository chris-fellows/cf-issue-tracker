using CFIssueTrackerCommon.Constants;
using CFIssueTrackerCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFIssueTrackerCommon.EntityReader
{
    /// <summary>
    /// Audit event type seed #1
    /// </summary>
    public class AuditEventTypeSeed1 : IEntityReader<AuditEventType>
    {
        public IEnumerable<AuditEventType> Read()
        {
            var list = new List<AuditEventType>()
            {
                new AuditEventType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = AuditEventTypeNames.Error
                },
                new AuditEventType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = AuditEventTypeNames.IssueCreated                    
                },
                new AuditEventType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = AuditEventTypeNames.IssueUpdated
                },
                new AuditEventType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = AuditEventTypeNames.IssuseCommentCreated
                },
                new AuditEventType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = AuditEventTypeNames.IssuseCommentUpdated
                },
                new AuditEventType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = AuditEventTypeNames.IssueStatusCreated
                },
                new AuditEventType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = AuditEventTypeNames.IssueStatusUpdated
                },
                new AuditEventType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = AuditEventTypeNames.IssueTypeCreated
                },
                new AuditEventType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = AuditEventTypeNames.IssueTypeUpdated
                },
                new AuditEventType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = AuditEventTypeNames.PasswordResetCreated
                },
                new AuditEventType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = AuditEventTypeNames.PasswordUpdated
                },
                new AuditEventType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = AuditEventTypeNames.ProjectCreated
                },
                new AuditEventType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = AuditEventTypeNames.ProjectUpdated
                },
                new AuditEventType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = AuditEventTypeNames.ProjectComponentCreated
                },
                new AuditEventType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = AuditEventTypeNames.ProjectComponentUpdated
                },
                new AuditEventType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = AuditEventTypeNames.UserCreated
                },
                new AuditEventType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = AuditEventTypeNames.UserLogInSuccess
                },
                new AuditEventType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = AuditEventTypeNames.UserLogOut
                },
                new AuditEventType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = AuditEventTypeNames.UserLogInError
                },
                new AuditEventType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = AuditEventTypeNames.UserUpdated
                }
            };

            return list;
        }
    }
}
