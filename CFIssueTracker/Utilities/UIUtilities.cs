using CFIssueTrackerCommon.Models;
using System.Drawing.Printing;

namespace CFIssueTracker.Utilities
{
    /// <summary>
    /// UI utilities
    /// </summary>
    public static class UIUtilities
    {
        /// <summary>
        /// Id for Any. Used for filters on properties where no filter is currently set.
        /// </summary>
        public static string AnyId = Guid.Empty.ToString();

        private const string _anyText = "Any";

        public static void AddAny(List<IssueType> issueTypes)
        {
            issueTypes.Insert(0, new IssueType()
            {
                Id = AnyId,
                Name = _anyText
            });
        }

        public static void AddAny(List<IssueStatus> issueStatuses)
        {
            issueStatuses.Insert(0, new IssueStatus()
            {
                Id = AnyId,
                Name = _anyText
            });
        }

        public static void AddAny(List<ProjectComponent> projectComponents)
        {
            projectComponents.Insert(0, new ProjectComponent()
            {
                Id = AnyId,
                Name = _anyText
            });
        }

        public static void AddAny(List<Project> projects)
        {
            projects.Insert(0, new Project()
            {
                Id = AnyId,
                Name = _anyText
            });
        }

        public static void AddAny(List<User> users)
        {
            users.Insert(0, new User()
            {
                Id = AnyId,
                Name = _anyText
            });
        }
    }
}
