using CFIssueTrackerCommon.Models;
using CFUtilities.CSV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFIssueTrackerCommon.EntityWriter
{
    public class CSVProjectComponentWriter : IEntityWriter<ProjectComponent>
    {
        private readonly CSVWriter<ProjectComponent> _csvWriter = new CSVWriter<ProjectComponent>();

        public CSVProjectComponentWriter(string file, Char delimiter, Encoding encoding)
        {
            _csvWriter.Delimiter = delimiter;
            _csvWriter.Encoding = encoding;
            _csvWriter.File = file;
        }

        public void Write(IEnumerable<ProjectComponent> projectComponents)
        {
            _csvWriter.AddColumn<string>("Id", i => i.Id, value => value.ToString());
            _csvWriter.AddColumn<string>("Name", i => i.Name, value => value.ToString());

            _csvWriter.Write(projectComponents);
        }
    }
}
