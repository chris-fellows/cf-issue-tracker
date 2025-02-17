﻿using System.ComponentModel.DataAnnotations;

namespace CFIssueTrackerCommon.Models
{
    /// <summary>
    /// Project
    /// </summary>
    public class Project
    {
        public string Id { get; set; } = String.Empty;

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = String.Empty;
    }
}
