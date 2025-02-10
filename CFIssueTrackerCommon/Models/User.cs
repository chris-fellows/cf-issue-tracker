using System.ComponentModel.DataAnnotations;

namespace CFIssueTrackerCommon.Models
{
    /// <summary>
    /// User
    /// </summary>
    public class User
    {
        public string Id { get; set; } = String.Empty;

        //[Required]
        public string Name { get; set; } = String.Empty;

        public string Email { get; set; } = String.Empty;

        public bool Active { get; set; } = true;
    }
}
