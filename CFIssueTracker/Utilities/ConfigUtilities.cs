namespace CFIssueTracker.Utilities
{
    public static class ConfigUtilities
    {
        /// <summary>
        /// Max size of image that can be uploaded
        /// </summary>
        public static long MaxUploadImageSize = 1024 * 300;

        /// <summary>
        /// Items per page on list pages (Audit Events, Issues etc0
        /// </summary>
        public static int ItemsPerListPage = 20;

        //public static string AuditEventTypesImageLocalFolder => Path.Combine(Environment.CurrentDirectory, "images", "audit_event_types");
        public static string AuditEventTypeImageLocalFolder => "D:\\Data\\Dev\\C#\\cf-issue-tracker\\CFIssueTracker\\wwwroot\\images\\audit_event_types";

        //public static string ProjectComponentImageLocalFolder => Path.Combine(Environment.CurrentDirectory, "images", "project_components");
        public static string ProjectComponentImageLocalFolder => "D:\\Data\\Dev\\C#\\cf-issue-tracker\\CFIssueTracker\\wwwroot\\images\\project_components";

        //public static string ProjectImageLocalFolder => Path.Combine(Environment.CurrentDirectory, "images", "projects");
        public static string ProjectImageLocalFolder => "D:\\Data\\Dev\\C#\\cf-issue-tracker\\CFIssueTracker\\wwwroot\\images\\projects";

        //public static string UserImageLocalFolder => Path.Combine(Environment.CurrentDirectory, "images", "users");
        public static string UserImageLocalFolder = "D:\\Data\\Dev\\C#\\cf-issue-tracker\\CFIssueTracker\\wwwroot\\images\\users";

        /// <summary>
        /// Local root folder where uploaded images are temporarily stored
        /// </summary>
        public static string ImageTempFilesRootFolder = Path.Combine(Path.GetTempPath(), "images");
    }
}
