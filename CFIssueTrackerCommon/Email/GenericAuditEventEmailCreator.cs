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
    /// Content for Generic Audit Event email. The email just indicates that the audit event happened.
    /// </summary>
    public class GenericAuditEventEmailCreator : IEmailCreator
    {
        private readonly IContentTemplateService _contentTemplateService;
        private readonly ISystemValueTypeService _systemValueTypeService;

        public static string CreatorName => "GenericAuditEvent";

        public GenericAuditEventEmailCreator(IContentTemplateService contentTemplateService,
                                ISystemValueTypeService systemValueTypeService)
        {
            _contentTemplateService = contentTemplateService;
            _systemValueTypeService = systemValueTypeService;
        }

        public string Name => CreatorName;

        public List<string> GetRecipientEmails(List<SystemValue> systemValues)
        {
            return new List<string>();
        }

        public string GetSubject(List<SystemValue> systemValues)
        {
            return "Audit Event";
        }

        public string GetBody(List<SystemValue> systemValues)
        {
            //var auditEventId = parameters["AuditEventId"];

            // Get content template
            var contentTemplate = _contentTemplateService.GetByNameAsync("Generic Audit Event Email").Result;

            // Replace placeholders in template
            var body = new StringBuilder(Encoding.UTF8.GetString(contentTemplate.Content));
            //body.Replace("{User.Email}", user.Email);            

            return body.ToString();
        }
    }
}
