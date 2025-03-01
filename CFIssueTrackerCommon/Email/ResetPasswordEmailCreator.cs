using CFIssueTrackerCommon.Constants;
using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;
using System.Text;
using System.Threading.Tasks;

namespace CFIssueTrackerCommon.Email
{
    /// <summary>
    /// Content for Reset Password email
    /// </summary>
    public class ResetPasswordEmailCreator : IEmailCreator
    {
        private readonly IContentTemplateService _contentTemplateService;
        private readonly IPasswordResetService _passwordResetService;
        private readonly ISystemValueTypeService _systemValueTypeService;
        private readonly IUserService _userService;

        public static string CreatorName => "ResetPassword";

        public ResetPasswordEmailCreator(IContentTemplateService contentTemplateService,
                            IPasswordResetService passwordResetService,
                            ISystemValueTypeService systemValueTypeService,
                            IUserService userService)
        {
            _contentTemplateService = contentTemplateService;
            _passwordResetService = passwordResetService;
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
            return "Reset Password";
        }

        public string GetBody(List<SystemValue> systemValues)
        {
            var systemValueTypePasswordResestId = _systemValueTypeService.GetByNameAsync(SystemValueTypeNames.PasswordResetId).Result;

            // Get password reset
            var passwordResetId = systemValues.First(sv => sv.TypeId == systemValueTypePasswordResestId.Id).Value;
            var passwordReset = _passwordResetService.GetByIdAsync(passwordResetId).Result;

            // Get user
            var user = _userService.GetById(passwordReset.UserId);

            // Get content template
            var contentTemplate = _contentTemplateService.GetByNameAsync("Reset Password Email").Result;

            // Replace placeholders in template
            var body = new StringBuilder(Encoding.UTF8.GetString(contentTemplate.Content));
            body.Replace("{User.Email}", user.Email);
            body.Replace("{PasswordReset.Url}", passwordReset.Url);

            return body.ToString();
        }
    }
}
