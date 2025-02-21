using CFIssueTrackerCommon.Enums;
using System.ComponentModel.DataAnnotations;

namespace CFIssueTrackerCommon.Models
{
    /// <summary>
    /// User
    /// </summary>
    public class User
    {
        [MaxLength(50)]
        public string Id { get; set; } = String.Empty;

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = String.Empty;

        [MaxLength(100)]
        public string Email { get; set; } = String.Empty;

        [Required]
        [MaxLength(100)]
        public string Password { get; set; } = String.Empty;

        [Required]
        [MaxLength(100)]
        public string Role { get; set; } = String.Empty;

        public bool Active { get; set; } = true;

        public UserTypes GetUserType() => Name.Equals("System") ?
                    UserTypes.System : UserTypes.Normal;
    }
}
