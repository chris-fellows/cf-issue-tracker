using CFIssueTrackerCommon.Models;
using CFUtilities.CSV;
using System.Text;

namespace CFIssueTrackerCommon.EntityWriter
{
    public class CSVUserWriter : IEntityWriter<User>
    {       
        private readonly CSVWriter<User> _csvWriter = new CSVWriter<User>();

        public CSVUserWriter(string file, Char delimiter, Encoding encoding)
        {
            _csvWriter.Delimiter = delimiter;
            _csvWriter.Encoding = encoding;
            _csvWriter.File = file;
        }

        public void Write(IEnumerable<User> users)
        {
            _csvWriter.AddColumn<string>("Id", u => u.Id, value => value.ToString());
            _csvWriter.AddColumn<string>("Name", u => u.Name, value => value.ToString());
            _csvWriter.AddColumn<string>("Email", u => u.Email, value => value.ToString());
            _csvWriter.AddColumn<bool>("Active", u => u.Active, value => value.ToString());

            _csvWriter.Write(users);
        }
    }
}
