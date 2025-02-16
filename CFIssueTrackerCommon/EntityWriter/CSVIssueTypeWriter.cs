using CFIssueTrackerCommon.Models;
using CFUtilities.CSV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFIssueTrackerCommon.EntityWriter
{
    public class CSVIssueTypeWriter : IEntityWriter<IssueType>
    {
        private readonly CSVWriter<IssueType> _csvWriter = new CSVWriter<IssueType>();

        public CSVIssueTypeWriter(string file, Char delimiter, Encoding encoding)
        {
            _csvWriter.Delimiter = delimiter;
            _csvWriter.Encoding = encoding;
            _csvWriter.File = file;
        }

        public void Write(IEnumerable<IssueType> issueTypes)
        {
            _csvWriter.AddColumn<string>("Id", i => i.Id, value => value.ToString());
            _csvWriter.AddColumn<string>("Name", i => i.Name, value => value.ToString());

            _csvWriter.Write(issueTypes);
        }
    }
}
