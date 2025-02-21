using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFIssueTrackerCommon.Constants
{
    public static class AuditEventTypeNames
    {
        public static string Error = "Error";
        public static string IssueCreated = "Issue created";
        public static string IssueUpdated = "Issue updated";
        public static string IssuseCommentCreated = "Issue comment created";
        public static string IssuseCommentUpdated = "Issue comment updated";
        public static string IssueStatusCreated = "Issue status created";
        public static string IssueStatusUpdated = "Issue status updated";
        public static string IssueTypeCreated = "Issue type created";
        public static string IssueTypeUpdated = "Issue type updated";
        public static string ProjectCreated = "Project created";
        public static string ProjectUpdated = "Project updated";
        public static string ProjectComponentCreated = "Project component created";
        public static string ProjectComponentUpdated = "Project component updated";
        public static string UserCreated = "User created";
        public static string UserUpdated = "User updated";
        public static string UserLogInSuccess = "User logged in";
        public static string UserLogOut = "User logged out";
        public static string UserLogInError = "User log in error";
    }
}
