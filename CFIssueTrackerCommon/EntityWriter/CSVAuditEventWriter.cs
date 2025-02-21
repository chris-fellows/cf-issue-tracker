using CFIssueTrackerCommon.Interfaces;
using CFIssueTrackerCommon.Models;
using CFUtilities.CSV;
using System.Text;

namespace CFIssueTrackerCommon.EntityWriter
{
    public class CSVAuditEventWriter : IEntityWriter<AuditEvent>
    {
        private readonly CSVWriter<AuditEvent> _csvWriter = new CSVWriter<AuditEvent>();

        private readonly IAuditEventTypeService _auditEventTypeService;

        public CSVAuditEventWriter(string file, Char delimiter, Encoding encoding,
                            IAuditEventTypeService auditEventTypeService)
        {
            _csvWriter.Delimiter = delimiter;
            _csvWriter.Encoding = encoding;
            _csvWriter.File = file;

            _auditEventTypeService = auditEventTypeService;
        }

        public void Write(IEnumerable<AuditEvent> auditEvents)
        {
            var auditEventTypes = _auditEventTypeService.GetAll();

            _csvWriter.AddColumn<string>("Id", i => i.Id, value => value.ToString());
            _csvWriter.AddColumn<DateTimeOffset>("CreatedDateTime", i => i.CreatedDateTime, value => value.ToString());
            _csvWriter.AddColumn<string>("AuditEventType", i => i.TypeId, value => auditEventTypes.First(u => u.Id == value).Name);                        

            _csvWriter.Write(auditEvents);            
        }
    }
}
