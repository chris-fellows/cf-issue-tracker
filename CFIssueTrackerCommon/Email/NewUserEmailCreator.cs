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
    /// Content for new user email
    /// </summary>
    public class NewUserEmailCreator : IEmailCreator
    {
        private readonly IContentTemplateService _contentTemplateService;
        private readonly ISystemValueTypeService _systemValueTypeService;
        private readonly IUserService _userService;

        public static string CreatorName => "NewUser";

        public NewUserEmailCreator(IContentTemplateService contentTemplateService,
                                ISystemValueTypeService systemValueTypeService,
                                IUserService userService)
        {
            _contentTemplateService = contentTemplateService;
            _systemValueTypeService = systemValueTypeService;
            _userService = userService;
        }

        public string Name => CreatorName;

        public List<string> GetRecipientEmails(List<SystemValue> systemValues)
        {
            return new List<string>();
        }

        public string GetSubject(List<SystemValue> systemValues)
        {
            return "Welcome to CF Issue Tracker";
        }

        public string GetBody(List<SystemValue> systemValues)
        {
            var systemValueTypeUserId = _systemValueTypeService.GetByNameAsync(SystemValueTypeNames.UserId).Result;

            var userId = systemValues.First(sv => sv.TypeId == systemValueTypeUserId.Id).Value;

            var user = _userService.GetById(userId);

            // Get content template
            var contentTemplate = _contentTemplateService.GetByNameAsync("New User Email").Result;

            // Replace placeholders in template
            var body = new StringBuilder(Encoding.UTF8.GetString(contentTemplate.Content));
            body.Replace("{User.Email}", user.Email);            

            return body.ToString();
        }
    }
}
