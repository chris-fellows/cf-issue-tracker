namespace CFIssueTrackerCommon.Interfaces
{
    /// <summary>
    /// Service for managing entity that has an Id and Name property
    /// </summary>
    /// <typeparam name="TEntityType"></typeparam>
    /// <typeparam name="TEntityIdType"></typeparam>
    public interface IEntityWithIdNameService<TEntityType, TEntityIdType> : IEntityWithIdService<TEntityType, TEntityIdType>
    {
        /// <summary>
        /// Gets entity by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<TEntityType?> GetByNameAsync(string name);
    }
}
