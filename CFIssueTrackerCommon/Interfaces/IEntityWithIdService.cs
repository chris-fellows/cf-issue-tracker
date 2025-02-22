namespace CFIssueTrackerCommon.Interfaces
{
    /// <summary>
    /// Service for managing entity that has an Id property.
    /// 
    /// It ensures that we keep consistent interfaces for entities
    /// </summary>
    /// <typeparam name="TEntityType"></typeparam>
    /// <typeparam name="TEntityIdType"></typeparam>
    public interface IEntityWithIdService<TEntityType, TEntityIdType>
    {
        List<TEntityType> GetAll();

        /// <summary>
        /// Gets all entities
        /// </summary>
        /// <returns></returns>
        Task<List<TEntityType>> GetAllAsync();

        /// <summary>
        /// Adds entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntityType> AddAsync(TEntityType entity);

        /// <summary>
        /// Updates entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntityType> UpdateAsync(TEntityType entity);

        /// <summary>
        /// Gets entity by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntityType?> GetByIdAsync(TEntityIdType id);

        /// <summary>
        /// Gets entities by Ids
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<List<TEntityType>> GetByIdsAsync(List<TEntityIdType> ids);

        Task DeleteByIdAsync(TEntityIdType id);
    }
}

