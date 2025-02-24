using CFIssueTrackerCommon.Models;
using CFUtilities.Utilities;
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

        public static void AddAny(List<AuditEventType> auditEventTypes)
        {
            auditEventTypes.Insert(0, new AuditEventType()
            {
                Id = AnyId,
                Name = _anyText
            });
        }

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

        /// <summary>
        /// Default date range filters
        /// </summary>
        /// <returns></returns>
        public static List<DateRangeFilter> GetDateRangeFilters()
        {
            return new List<DateRangeFilter>()
            {
                new DateRangeFilter()
                {
                    Id = "1",
                    Name = "All time"
                },
                new DateRangeFilter()
                {
                    Id = "2",
                    Name = "Today",
                    FromDate = DateTimeUtilities.GetTodayStart()
                },
                new DateRangeFilter()
                {
                    Id = "3",
                    Name = "Current month",
                    FromDate = DateTimeUtilities.GetMonthStart()
                },
                new DateRangeFilter()
                {
                    Id = "4",
                    Name = "Current year",
                    FromDate = DateTimeUtilities.GetYearStart()
                }
            };
        }
    }
}
