using CFIssueTrackerCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFIssueTrackerCommon.Interfaces
{
    /// <summary>
    /// Interface for audit event metrics
    /// </summary>
    public interface IAuditEventMetricService
    { 
        /// <summary>
        /// Gets audit event count by one or more property dimensions.
        /// </summary>
        /// <param name="issueFilter"></param>
        /// <param name="propertyNameDimensions"></param>
        /// <param name="excludeZeroValues">Exclude zero metrics (If false then returns every combination of dimensions)</param>
        /// <returns></returns>
        Task<Metrics> GetAuditEventCountAsync(AuditEventFilter filter, List<string> propertyNameDimensions, bool excludeZeroValues);
    }
}
