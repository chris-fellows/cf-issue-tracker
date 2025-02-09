using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace CFIssueTrackerCommon.Interfaces
{
    public interface IEntityWithIdService<TEntityType, TEntityIdType>
    {
        Task<List<TEntityType>> GetAllAsync();

        Task<TEntityType> AddAsync(TEntityType entity);

        Task<TEntityType?> GetByIdAsync(TEntityIdType id);
    }
}
