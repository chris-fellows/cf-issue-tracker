using CFIssueTrackerCommon.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFIssueTrackerCommon.Models
{
    /// <summary>
    /// Metrics type
    /// </summary>
    public class MetricsType
    {
        [MaxLength(50)]
        public string Id { get; set; } = String.Empty;

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = String.Empty;

        /// <summary>
        /// Entity type for statistics that metrics relate to
        /// </summary>
        public EntityTypes EntityType { get; set; }
        
        /// <summary>
        /// Property names for dimension(s). Just store as a comma separated list in the DB.
        /// </summary>
        [MaxLength(100)]
        public string DimensionPropertyNames { get; set; } = String.Empty;

        ///// <summary>
        ///// Chart type name(s). Just store as a comma separated list in the DB.
        ///// </summary>
        //public string ChartTypeNames { get; set; } = String.Empty;

        [Required]
        [MaxLength(50)]
        public string Color { get; set; } = String.Empty;

        [Required]
        [MaxLength(50)]
        public string ImageSource { get; set; } = String.Empty;
    }
}
