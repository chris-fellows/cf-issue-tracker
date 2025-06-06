﻿using System.ComponentModel.DataAnnotations;

namespace CFIssueTrackerCommon.Models
{
    /// <summary>
    /// Issue status
    /// </summary>
    public class IssueStatus
    {
        [MaxLength(50)]
        public string Id { get; set; } = String.Empty;

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = String.Empty;

        [Required]
        [MaxLength(50)]
        public string Color { get; set; } = String.Empty;
    }
}
