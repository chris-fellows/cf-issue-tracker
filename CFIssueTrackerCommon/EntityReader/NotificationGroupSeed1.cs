using CFIssueTrackerCommon.Email;
using CFIssueTrackerCommon.Models;
using System;
using CFIssueTrackerCommon.Constants;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFIssueTrackerCommon.EntityReader
{
    public class NotificationGroupSeed1 : IEntityReader<NotificationGroup>
    {
        public IEnumerable<NotificationGroup> Read()
        {
            var list = new List<NotificationGroup>()
            {
                new NotificationGroup()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = NotificationGroupNames.GenericAuditEvent,
                    EmailNotificationConfigs = new List<EmailNotificationConfig>()
                    {
                        new EmailNotificationConfig()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Creator = GenericAuditEventEmailCreator.CreatorName,
                            RecipientEmails = ""
                        }
                    }
                },

                new NotificationGroup()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = NotificationGroupNames.IssueAssigned,
                    EmailNotificationConfigs = new List<EmailNotificationConfig>()
                    {
                        new EmailNotificationConfig()
                        {
                            Id= Guid.NewGuid().ToString(),
                            Creator = IssueAssignedEmailCreator.CreatorName,
                            RecipientEmails = ""
                        }
                    }
                },
                new NotificationGroup()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = NotificationGroupNames.NewUser,
                    EmailNotificationConfigs = new List<EmailNotificationConfig>()
                    {
                        new EmailNotificationConfig()
                        {
                            Id= Guid.NewGuid().ToString(),
                            Creator = NewUserEmailCreator.CreatorName,
                            RecipientEmails = ""
                        }
                    }
                },
                new NotificationGroup()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = NotificationGroupNames.ResetPassword,
                    EmailNotificationConfigs = new List<EmailNotificationConfig>()
                    {
                        new EmailNotificationConfig()
                        {
                            Id= Guid.NewGuid().ToString(),
                            Creator = ResetPasswordEmailCreator.CreatorName,
                            RecipientEmails = ""
                        }
                    }
                }
            };

            return list;
        }
    }
}
