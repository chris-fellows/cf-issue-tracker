using System.ComponentModel.DataAnnotations;

namespace CFIssueTrackerCommon.Models
{
    public class LoginCredentials
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide a username")]
        public string? Username { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide a password")]
        public string? Password { get; set; }
    }
}
