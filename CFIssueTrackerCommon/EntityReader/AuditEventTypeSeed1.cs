using CFIssueTrackerCommon.Constants;
using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
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
        private readonly INotificationGroupService _notificationGroupService;

        public AuditEventTypeSeed1(INotificationGroupService notificationGroupService)
        {
            _notificationGroupService = notificationGroupService;
        }

        public IEnumerable<AuditEventType> Read()
        {
            var notificationGroups = _notificationGroupService.GetAll();

            var list = new List<AuditEventType>()
            {
                new AuditEventType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = AuditEventTypeNames.Error,
                    Color = Color.Red.ToArgb().ToString(),
                    ImageSource = "audit_event_type.png",                     
                },
                new AuditEventType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = AuditEventTypeNames.IssueAssigned,
                    Color = Color.Red.ToArgb().ToString(),
                    ImageSource = "audit_event_type.png",
                    NotificationGroups = new List<NotificationGroupReference>()
                    {
                        new NotificationGroupReference()
                        {
                            Id = Guid.NewGuid().ToString(),
                            NotificationGroupId = notificationGroups.First(ng => ng.Name == NotificationGroupNames.IssueAssigned).Id                            
                        }
                    }
                },
                new AuditEventType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = AuditEventTypeNames.IssueCreated,
                    Color = Color.Red.ToArgb().ToString(),
                    ImageSource = "audit_event_type.png"
                },
                new AuditEventType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = AuditEventTypeNames.IssueUpdated,
                    Color = Color.Red.ToArgb().ToString(),
                    ImageSource = "audit_event_type.png"
                },
                new AuditEventType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = AuditEventTypeNames.IssuseCommentCreated,
                    Color = Color.Red.ToArgb().ToString(),
                    ImageSource = "audit_event_type.png"
                },
                new AuditEventType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = AuditEventTypeNames.IssuseCommentUpdated,
                    Color = Color.Red.ToArgb().ToString(),
                    ImageSource = "audit_event_type.png"
                },
                new AuditEventType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = AuditEventTypeNames.IssueStatusCreated,
                    Color = Color.Red.ToArgb().ToString(),
                    ImageSource = "audit_event_type.png"
                },
                new AuditEventType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = AuditEventTypeNames.IssueStatusUpdated,
                    Color = Color.Red.ToArgb().ToString(),
                    ImageSource = "audit_event_type.png"
                },
                new AuditEventType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = AuditEventTypeNames.IssueTypeCreated,
                    Color = Color.Red.ToArgb().ToString(),
                    ImageSource = "audit_event_type.png"
                },
                new AuditEventType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = AuditEventTypeNames.IssueTypeUpdated,
                    Color = Color.Red.ToArgb().ToString(),
                    ImageSource = "audit_event_type.png"
                },
                new AuditEventType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = AuditEventTypeNames.PasswordResetCreated,
                    Color = Color.Red.ToArgb().ToString(),
                    ImageSource = "audit_event_type.png",
                    NotificationGroups = new List<NotificationGroupReference>()
                    {
                        new NotificationGroupReference()
                        {
                            Id = Guid.NewGuid().ToString(),
                            NotificationGroupId = notificationGroups.First(ng => ng.Name == NotificationGroupNames.ResetPassword).Id
                        }
                    }
                },
                new AuditEventType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = AuditEventTypeNames.PasswordUpdated,
                    Color = Color.Red.ToArgb().ToString(),
                    ImageSource = "audit_event_type.png"
                },
                new AuditEventType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = AuditEventTypeNames.ProjectCreated,
                    Color = Color.Red.ToArgb().ToString(),
                    ImageSource = "audit_event_type.png"
                },
                new AuditEventType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = AuditEventTypeNames.ProjectUpdated,
                    Color = Color.Red.ToArgb().ToString(),
                    ImageSource = "audit_event_type.png"
                },
                new AuditEventType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = AuditEventTypeNames.ProjectComponentCreated,
                    Color = Color.Red.ToArgb().ToString(),
                    ImageSource = "audit_event_type.png"
                },
                new AuditEventType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = AuditEventTypeNames.ProjectComponentUpdated,
                    Color = Color.Red.ToArgb().ToString(),
                    ImageSource = "audit_event_type.png"
                },
                new AuditEventType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = AuditEventTypeNames.UserCreated,
                    Color = Color.Red.ToArgb().ToString(),
                    ImageSource = "audit_event_type.png",
                    NotificationGroups = new List<NotificationGroupReference>()
                    {
                        new NotificationGroupReference()
                        {
                            Id = Guid.NewGuid().ToString(),
                            NotificationGroupId = notificationGroups.First(ng => ng.Name == NotificationGroupNames.NewUser).Id
                        }
                    }
                },
                new AuditEventType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = AuditEventTypeNames.UserLogInSuccess,
                    Color = Color.Red.ToArgb().ToString(),
                    ImageSource = "audit_event_type.png"
                },
                new AuditEventType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = AuditEventTypeNames.UserLogOut,
                    Color = Color.Red.ToArgb().ToString(),
                    ImageSource = "audit_event_type.png"
                },
                new AuditEventType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = AuditEventTypeNames.UserLogInError,
                    Color = Color.Red.ToArgb().ToString(),
                    ImageSource = "audit_event_type.png"
                },
                new AuditEventType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = AuditEventTypeNames.UserUpdated,
                    Color = Color.Red.ToArgb().ToString(),
                    ImageSource = "audit_event_type.png"
                }
            };

            return list;
        }
    }
}
