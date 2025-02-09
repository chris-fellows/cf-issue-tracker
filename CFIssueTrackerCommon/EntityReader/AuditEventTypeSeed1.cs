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
                    Name = "Error"
                },
                new AuditEventType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Issue created"                    
                },
                new AuditEventType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Issue updated"
                },
                  new AuditEventType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Issue status created"
                },
                new AuditEventType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Issue status updated"
                },
                new AuditEventType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Issue type created"
                },
                new AuditEventType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Issue type updated"
                },
                new AuditEventType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Project created"
                },
                new AuditEventType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Project updated"
                },
                new AuditEventType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Project component created"
                },
                new AuditEventType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Project component updated"
                },
                new AuditEventType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "User created"
                },
                new AuditEventType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "User logged in"
                },
                new AuditEventType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "User logged out"
                },
                new AuditEventType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "User updated"
                }
            };

            return list;
        }
    }
}
