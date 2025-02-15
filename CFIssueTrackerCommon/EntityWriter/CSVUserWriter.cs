using CFIssueTrackerCommon.Models;
using System.Text;

namespace CFIssueTrackerCommon.EntityWriter
{
    public class CSVUserWriter : IEntityWriter<User>
    {
        private readonly string _file;
        private readonly Char _delimiter;

        public CSVUserWriter(string file, Char delimiter)
        {
            _file = file;
            _delimiter = delimiter;
        }

        public void Write(IEnumerable<User> users)
        {
            if (File.Exists(_file))
            {
                File.Delete(_file);
            }

            using (var streamWriter = new StreamWriter(_file, true, Encoding.UTF8))
            {
                streamWriter.WriteLine($"Id{_delimiter}Name{_delimiter}Email");

                foreach (var user in users)
                {
                    Write(user, streamWriter);
                }
            }
        }

        private void Write(User user, StreamWriter streamWriter)
        {
            streamWriter.WriteLine($"{user.Id}{_delimiter}{user.Name}{_delimiter}{user.Email}");
        }
    }
}
