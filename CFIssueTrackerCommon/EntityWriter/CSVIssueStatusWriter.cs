using CFIssueTrackerCommon.Models;
using CFUtilities.CSV;
using System.Text;

namespace CFIssueTrackerCommon.EntityWriter
{
    public class CSVIssueStatusWriter : IEntityWriter<IssueStatus>
    {
        private readonly CSVWriter<IssueStatus> _csvWriter = new CSVWriter<IssueStatus>();

        public CSVIssueStatusWriter(string file, Char delimiter, Encoding encoding)
        {
            _csvWriter.Delimiter = delimiter;
            _csvWriter.Encoding = encoding;
            _csvWriter.File = file;
        }

        public void Write(IEnumerable<IssueStatus> issueStatuses)
        {
            _csvWriter.AddColumn<string>("Id", i => i.Id, value => value.ToString());
            _csvWriter.AddColumn<string>("Name", i => i.Name, value => value.ToString());

            _csvWriter.Write(issueStatuses);
        }
    }
}
