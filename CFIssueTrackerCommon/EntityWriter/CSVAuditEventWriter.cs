using CFIssueTrackerCommon.Models;
using CFUtilities.CSV;
using System.Text;

namespace CFIssueTrackerCommon.EntityWriter
{
    public class CSVAuditEventWriter : IEntityWriter<AuditEvent>
    {
        private readonly CSVWriter<AuditEvent> _csvWriter = new CSVWriter<AuditEvent>();

        public CSVAuditEventWriter(string file, Char delimiter, Encoding encoding)
        {
            _csvWriter.Delimiter = delimiter;
            _csvWriter.Encoding = encoding;
            _csvWriter.File = file;
        }

        public void Write(IEnumerable<AuditEvent> auditEvents)
        {
            _csvWriter.Write(auditEvents);
        }
    }
}
