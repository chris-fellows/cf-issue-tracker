using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFIssueTrackerCommon.EntityReader
{
    /// <summary>
    /// Reads entity from some format (E.g. Memory, JSON file, XML file etc)
    /// </summary>
    /// <typeparam name="TEntityType"></typeparam>
    public interface IEntityReader<TEntityType>
    {
        IEnumerable<TEntityType> Read();
    }
}
