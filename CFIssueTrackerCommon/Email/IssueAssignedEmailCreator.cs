using CFIssueTrackerCommon.Constants;
using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFIssueTrackerCommon.Email
{
    /// <summary>
    /// Content for Issue Assigned email
    /// </summary>
    public class IssueAssignedEmailCreator : IEmailCreator
    {
        private readonly IContentTemplateService _contentTemplateService;
        private readonly IIssueService _issueService;
        private readonly ISystemValueTypeService _systemValueTypeService;
        private readonly IUserService _userService;

        public static string CreatorName => "IssueAssigned";

        public IssueAssignedEmailCreator(IContentTemplateService contentTemplateService,
                            IIssueService issueService,
                            ISystemValueTypeService systemValueTypeService,
                            IUserService userService)
        {
            _contentTemplateService = contentTemplateService;
            _issueService = issueService;
            _systemValueTypeService = systemValueTypeService;
            _userService = userService;
        }

        public string Name => CreatorName;

        public List<string> GetRecipientEmails(List<SystemValue> systemValues)
        {
            // Get issue
            var issue = GetIssue(systemValues);

            if (String.IsNullOrEmpty(issue.AssignedUserId)) return new();

            // Get assigned user
            var assignedUser = _userService.GetById(issue.AssignedUserId);

            return (assignedUser == null || String.IsNullOrEmpty(issue.AssignedUserId)) ? new() :
                            new List<string>() { assignedUser.Email };
        }

        public string GetSubject(List<SystemValue> systemValues)
        {
            return "Issue Assigned";
        }

        public string GetBody(List<SystemValue> systemValues)
        {
            // Get issue
            var issue = GetIssue(systemValues);

            // Get assigned user
            var assignedUser = _userService.GetById(issue.AssignedUserId);

            // Get content template
            var contentTemplate = _contentTemplateService.GetByNameAsync("Issuse Assigned Email").Result;

            // Replace placeholders in template
            var body = new StringBuilder(Encoding.UTF8.GetString(contentTemplate.Content));
            body.Replace("{AssignedUser.Email}", assignedUser.Email);
            body.Replace("{Issue.Reference}", issue.Reference);
            body.Replace("{Issue.Description}", issue.Description);            
            
            return body.ToString();
        }

        private Issue? GetIssue(List<SystemValue> systemValues)
        {
            var systemValueTypeIssueId = _systemValueTypeService.GetByNameAsync(SystemValueTypeNames.IssueId).Result;

            // Get issue
            var issueId = systemValues.First(sv => sv.TypeId == systemValueTypeIssueId.Id).Value;
            var issue = _issueService.GetByIdAsync(issueId).Result;

            return issue;
        }
    }
}
